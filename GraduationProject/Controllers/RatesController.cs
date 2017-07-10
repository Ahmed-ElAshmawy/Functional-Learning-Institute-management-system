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

namespace GraduationProject.Controllers
{
    public class RatesController : ApiController
    {
        RateService _dbService = new RateService();
        [ResponseType(typeof(Rate))]
        public IHttpActionResult GetRate(string therapistId)
        {
            var rateList = _dbService.GetAll(therapistId);
            if (rateList != null)
            {
                return Ok(rateList);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Rates
        [ResponseType(typeof(Rate))]
        public IHttpActionResult PostRate(RateDto rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int newRate = _dbService.AddRate(rate);
            if (newRate == 0)
                return Conflict();
            return Ok(new { Message = "Created", Model = rate });
        }
        [ResponseType(typeof(Rate))]
        public IHttpActionResult Delete(string id, string therapistId)
        {
            int result = _dbService.DeleteRate(id, therapistId);
            if (result == 1)
            {
                return Ok(new { Message = "Deleted" });
            }
            else
            {
                return BadRequest();
            }
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