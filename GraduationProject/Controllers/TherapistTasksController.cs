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
using GraduationProject.BL;
using System.Collections;

namespace GraduationProject.Controllers
{
    public class TherapistTasksController : ApiController
    {
        TherapistTaskService _dbService = new TherapistTaskService();
        // GET: api/TherapistTasks
        public IEnumerable GetTherapistTasks()
        {
            return _dbService.GetAll();
        }


        // GET: api/TherapistTasks/5
        [ResponseType(typeof(TherapistTaskDto))]
        public IHttpActionResult GetTherapistTask(string id)
        {
            var Task = _dbService.GetById(id);
            if (Task == null)
            {
                return NotFound();
            }

            return Ok(Task);
        }

        // PUT: api/TherapistTasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTherapistTask(string id, TherapistTaskDto therapistTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int UpdatedTask = _dbService.Update(therapistTask, id);
            if (UpdatedTask == 1)
                return Ok(new { Message = "Updated" });
            return NotFound();
        }

        // POST: api/TherapistTasks
        [ResponseType(typeof(TherapistTaskDto))]
        public IHttpActionResult PostTherapistTask(TherapistTaskDto TherapistTask)
        {
            int CreatedTask = _dbService.Create(TherapistTask);
            if (CreatedTask == 0)
                return Conflict();
            return Ok(new { Message = "Created", Id = TherapistTask.Id });
        }

        // DELETE: api/TherapistTasks/5
        [ResponseType(typeof(TherapistTaskDto))]
        public IHttpActionResult DeleteTherapistTask(string id)
        {
            return NotFound();
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