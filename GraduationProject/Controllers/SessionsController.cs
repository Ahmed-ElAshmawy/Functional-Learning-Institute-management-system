using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using GraduationProject.BL;
using GraduationProject.Models;
using GraduationProject.Models.dto;

namespace GraduationProject.Controllers
{

    // [Authorize(Roles = "Parent")]
    public class SessionsController : ApiController
    {
        public SessionsController()
        {
            _dbService = new SessionService();
        }
        private SessionService _dbService;

        /// <summary>
        /// GetAll Sessions
        /// </summary>
        /// <returns>All Sessions</returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetAll")]
        public IHttpActionResult GetAllSessions()
        {
            return Ok(_dbService.GetAll());
        }
        //get schedulesession for therapist
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetScheduleSessionsForTherapist")]
        public IHttpActionResult GetScheduleSessionsTherapist(DateTime date, string id)
        {
            return Ok(_dbService.GetScheduleSessionsTherapist(date, id));
        }


        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetSessionByDate")]
        public IHttpActionResult GetSessionByDate(DateTime date)
        {
            return Ok(_dbService.GetSessionByDate(date));
        }
        //get schedulesession for child
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetScheduleSessionsForChild")]
        public IHttpActionResult GetScheduleSessionsChild(DateTime date, string id)
        {
            return Ok(_dbService.GetScheduleSessionsChild(date, id));
        }

        /// <summary>
        /// Get All session on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetByDate")]
        public IEnumerable GetSessionsByDate(DateTime date)
        {
            return _dbService.GetByDate(date);
        }

        /// <summary>
        /// Get all child session on specific date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetAllChildByDate")]
        public IEnumerable GetByDateForChild(DateTime date, string childId)
        {
            return _dbService.GetByDateForChild(date, childId);
        }
        /// <summary>
        /// Get All Child Recent Sessions
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetRecentChild")]
        public IEnumerable GetRecentChild(string childId)
        {
            return _dbService.GetByDateForChild(childId);
        }

        /// <summary>
        ///  Get all therapist sessions on specific date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="therapistId"></param>
        /// <returns></returns>
        [Route("api/Sessions/GetAllTherapistByDate")]
        public IEnumerable GetByDateForTherapist(DateTime date, string therapistId)
        {
            return _dbService.GetByDateForTherapist(date, therapistId);
        }

        /// <summary>
        ///  Get all therapist Recent sessions
        /// </summary>
        /// <param name="therapistId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetRecentTherapist")]
        public IEnumerable GetRecentTherapist(string therapistId)
        {
            return _dbService.GetByDateForTherapist(therapistId);
        }

        [Route("api/Sessions/GetToDay")]
        public IHttpActionResult GetToDaySessions()
        {
            return Ok(_dbService.GetByDate(DateTime.Now));
        }


        [Route("api/Sessions/GetByRecentDate")]
        public IHttpActionResult GetByRecentDate()
        {
            return Ok(_dbService.GetByRecentDate());
        }

        // GET: api/Sessions/5
        [Route("api/Sessions/GetSession/{id}")]
        [ResponseType(typeof(Session))]
        public IHttpActionResult GetSession(string id)
        {
            var session = _dbService.GetById(id);
            if (session != null)
            {
                return Ok(session);
            }
            return NotFound();
        }

        // GET: api/Sessions/5
        [Route("api/Sessions/GetUpdateSession/{id}")]
        [ResponseType(typeof(Session))]
        public IHttpActionResult GetUpdateSession(string id)
        {
            var session = _dbService.GetToUpdate(id);
            if (session != null)
            {
                return Ok(session);
            }
            return NotFound();
        }

        // PUT: api/Sessions/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Sessions/PutSession")]
        public IHttpActionResult PutSession(string id, bool status, SessionDto session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != session.Id)
            {
                return BadRequest();
            }
            var result = _dbService.Update(session, status);
            if (result == -1)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Updated" });
        }

        // POST: api/Sessions
        [ResponseType(typeof(Session))]
        public IHttpActionResult PostSession(SessionDto session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Create(session);
            if (result == 1)
            {
                return Ok(new { Message = "Created", Model = session });

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Sessions/5
        [ResponseType(typeof(Session))]
        public IHttpActionResult DeleteSession(string id)
        {
            var result = _dbService.Delete(id);
            if (result == 1)
                return Ok(new { Message = "Deleted" });
            //db.Sessions.Remove(session);
            //db.SaveChanges();
            return NotFound();
        }


        //deleteSession
        [Route("api/Sessions/DeleteSessionByDateAndSlot")]
        public IHttpActionResult DeleteSessionByDateAndSlot(DateTime date, string childId, string slotId)
        {

            var result = _dbService.DeleteSessionByDateAndSlot(date, childId, slotId);
            if (result == 1)
                return StatusCode(HttpStatusCode.NoContent);
            return Ok(new { Message = "Deleted" });
        }

        //Assign new therapist for the session
        [Route("api/Sessions/PutSessionByAssignNewTherapist")]
        public IHttpActionResult PutSessionByAssignNewTherapist(DateTime sessiondate, string therapistId, string slotId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.AssignTherapist(sessiondate, therapistId, slotId);
            if (result == -1)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Updated" });
        }




        // create group
        [Route("api/Sessions/PutSessionForGroup")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSessionForGroup(string id, [FromUri] string[] childrenId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.CreateGroup(id, childrenId);
            if (result == -1)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Updated" });
        }


        [Route("api/Sessions/CancelSession")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult CancelSession(string id, bool status)
        {
            var result = _dbService.Cancel(id, status);
            if (result == -1)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Canceled" });
        }
        [Route("api/Sessions/PutChangeToFinish")]
        public IHttpActionResult PutChangeToFinish(string id)
        {
            var result = _dbService.ChangeToFinish(id);
            if (result == -1)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Updated" });
        }

        [Route("api/Sessions/GetSessionFinshing")]
        public IEnumerable GetSessionFinshing()
        {
            return _dbService.SessionFinshing();
        }
        [ResponseType(typeof(IEnumerable))]
        [Route("api/Sessions/GetSessionsForChild")]
        public IHttpActionResult GetSessionsForChild()
        {
            return Ok(_dbService.GetSessionsForChild());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}