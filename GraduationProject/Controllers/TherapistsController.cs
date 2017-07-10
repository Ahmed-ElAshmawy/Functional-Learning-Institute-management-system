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
using AutoMapper;
using GraduationProject.Models;
using System.Threading.Tasks;
using System.Collections;

namespace GraduationProject.Controllers
{

    public class TherapistsController : ApiController
    {
        TherapistService _dbService = new TherapistService();
        // GET: api/Therapists
        public IHttpActionResult GetUsers()
        {
            var TherapistList = _dbService.GetAll();
            return Ok(TherapistList);
        }

        // GET: api/Therapists/5
        [ResponseType(typeof(Therapist))]
        public IHttpActionResult GetTherapist(string id)
        {
            BaseTherapistDto Therapist = _dbService.GetById(id);
            if (Therapist == null)
            {
                return NotFound();
            }
            return Ok(Therapist);
        }

        // PUT: api/Therapists/5
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutTherapist(string id, BaseTherapistDto Therapist)
        {
            int result;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id.ToUpper() != Therapist.Id.ToUpper())
            {
                return NotFound();
            }
            result = _dbService.Update(Therapist, id);
            if (result == 0)
            {
                return NotFound();
            }
            else if (result == 1)
            {
                return Ok(new { Message = "Updated" });
            }
            else if (result == 2)
            {
                return BadRequest("Email is not availiable");
            }
            else
            {
                return BadRequest();
            }


        }

        //POST: api/Therapists
        [ResponseType(typeof(Therapist))]
        public async Task<IHttpActionResult> PostTherapist(CreateTherapistDto therapist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int result = await _dbService.Create(therapist);
            if (result == 1)
            {
                return Ok(therapist);
            }
            else if (result == 2)
            {
                return BadRequest("Email is not available");
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // DELETE: api/Therapists/5
        [ResponseType(typeof(Therapist))]
        public IHttpActionResult DeleteTherapist(string id)
        {
            int Flag = _dbService.Delete(id);
            if (Flag == 0)
            {
                return NotFound();
            }
            else if (Flag == -1)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { Message = "Deleted" });
            }
        }
        [Route("api/Therapists/PutTherapistRerieve")]
        [ResponseType(typeof(Therapist))]
        public IHttpActionResult TherapistRerieve(string id)
        {
            int Flag = _dbService.RerieveTherapist(id);
            if (Flag == 0)
            {
                return NotFound();
            }
            else if (Flag == -1)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { Message = "Updated" });
            }
        }

        [Route("api/Therapists/GetRetrieveTherapists")]
        public IHttpActionResult GetRetrieveTherapists()
        {
            var therapistList = _dbService.RetrieveList();
            return Ok(therapistList);
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