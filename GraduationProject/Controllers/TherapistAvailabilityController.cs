using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GraduationProject.Models;
using GraduationProject.Models.dto;
using GraduationProject.BL;
using System.Collections;

namespace GraduationProject.Controllers
{
    public class TherapistAvailabilityController : ApiController
    {
        private TherapistAvailabilityService _dbService;

        public TherapistAvailabilityController()
        {
            _dbService = new TherapistAvailabilityService();
        }
        // GET: api/TherapistAvailability
        [Route("api/TherapistAvailability")]
        public IHttpActionResult GetTherapistAvailability()
        {
            return Ok(_dbService.GetAll());
        }
        /// <summary>
        /// Get All TherapistAvailability on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/TherapistAvailability/GetByDate")]
        public IEnumerable GetTherapistAvailabilityByDate(DateTime date)
        {
            return _dbService.GetByDate(date);
        }

        /// <summary>
        /// Get All TherapistAvailability on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="slots">Date</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/TherapistAvailability/GetBySlot")]
        public IEnumerable GetTherapistAvailabilityByDate(DateTime date, [FromUri] string[] slots)
        {
            return _dbService.GetByAvailabileSlots(date, slots);
        }
        /// <summary>
        /// GetByTherapistAvailability
        /// </summary>
        /// <param name="date"></param>
        /// <param name="TherapistId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/TherapistAvailability/GetAvailabilityForTherapistByDateAndId")]
        public IEnumerable GetByAvailabilityForTherapist(DateTime date, string TherapistId, bool? IsBusy)
        {
            return _dbService.GetByTherapistAvailability(date, TherapistId, IsBusy);
        }

        /// <summary>
        /// GetByAvailabilityForTherapist
        /// </summary>
        /// <param name="TherapistId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/TherapistAvailability/GetAvailabilityForTherapistById")]
        public IEnumerable GetAvailabilityForTherapistById(string id)
        {
            return _dbService.GetTherapistAvailabilityById(id);
        }
        /// <summary>
        /// Update TherapistAvailability
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        // PUT: api/TherapistAvailability/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTherapistsAvailability(string id, TherapistAvailabilityDto therapistavailabilityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Update(therapistavailabilityDto, id);
            if (result == 0)
            {
                return NotFound();
            }
            else if (result == -1)
            {
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Updated" });
        }
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTherapistsAvailabilityState(string id, bool IsBusy)
        {
            var result = _dbService.UpdateState(id, IsBusy);
            if (result == 0)
            {
                return NotFound();
            }
            else if (result == -1)
            {
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "Updated" });
        }


        // POST: api/TherapistAvailability
        [ResponseType(typeof(TherapistAvailability))]
        [Route("api/TherapistAvailability/Post")]
        public IHttpActionResult PostTherapistAvailabilityDto(CreateTherapistAvailabilityDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = _dbService.CreateAvailability(model);
            if (res == 1)
                return Ok(new { Message = "Created", Model = model });
            return BadRequest(ModelState);
        }
        // DELETE: api/TherapistAvailability/5
        [ResponseType(typeof(TherapistAvailability))]
        public IHttpActionResult DeleteTherapistsAvailability(string id)
        {
            var result = _dbService.Delete(id);
            if (result == 0)
                return NotFound();
            else if (result == -1)
                return BadRequest();
            return Ok(new { Message = "Deleted" });
        }
        // GET: api/TherapistAvailability/5
        [ResponseType(typeof(TherapistAvailability))]
        [Route("api/TherapistAvailability/GetById")]
        public IHttpActionResult GetTherapistsAvailabilityById(string id)
        {
            TherapistAvailabilityDto therapistAvailabilityDto = _dbService.GetById(id);
            if (therapistAvailabilityDto == null)
            {
                return NotFound();
            }
            return Ok(therapistAvailabilityDto);
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
