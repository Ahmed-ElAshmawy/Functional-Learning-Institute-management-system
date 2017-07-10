using GraduationProject.BL;
using GraduationProject.Models;
using GraduationProject.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GraduationProject.Controllers
{
    public class ComplaintController : ApiController
    {
        ComplaintService _dbService = new ComplaintService();
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult GetComplaint(string userId)
        {
            var complaintList = _dbService.GetAllForUser(userId);
            if (complaintList != null)
            {
                return Ok(complaintList);
            }
            else
            {
                return NotFound();
            }
        }
        [ResponseType(typeof(Complaint))]
        [Route("api/Complaint/GetAll")]
        public IHttpActionResult GetAllComplaint(string id)
        {
            var complaintList = _dbService.GetAll(id);
            if (complaintList != null)
            {
                return Ok(complaintList);
            }
            else
            {
                return NotFound();
            }
        }
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult GetComplaintById(string id)
        {

            ComplaintDto complaint = _dbService.GetComplaint(id);
            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        // POST: api/Rates
        [ResponseType(typeof(Complaint))]
        public async Task<IHttpActionResult> PostComplaint(ComplaintDto complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newComplaint = _dbService.AddComplaint(complaint);
            if (newComplaint == null)
                return Conflict();
            return CreatedAtRoute("DefaultApi", new { id = complaint.Id }, complaint);
        }
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult Delete(string id)
        {
            int result = _dbService.DeleteComplaint(id);
            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        ///controller
        // put: api/Complaint

        [Route("api/Complaints/PutComplaint")]
        [ResponseType(typeof(Complaint))]
        public IHttpActionResult PutComplaint(string id, string answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int newComplaint = _dbService.UpdateComplaint(id, answer);
            if (newComplaint == 0)
                return NotFound();
            return StatusCode(HttpStatusCode.NoContent);
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
