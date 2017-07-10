using GraduationProject.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace GraduationProject.Controllers
{
    public class SlotController : ApiController
    { 
      SlotsService _dbService= new SlotsService();
       [ResponseType(typeof(IEnumerable))]
        [Route("api/slots")]
        public IHttpActionResult GetAllSlots()
        {
            return Ok(_dbService.GetAll());
        }

        [ResponseType(typeof(IEnumerable))]
        [Route("api/GetSlotsforChild")]
        public IHttpActionResult GetSlotsforChild(string id,DateTime date)
        {
            return Ok(_dbService.GetSlotsforChild(id,date));
        }

        [ResponseType(typeof(IEnumerable))]
        [Route("api/GetSlotsforTherapist")]
        public IHttpActionResult GetSlotsforTherapist(string id, DateTime date)
        {
            return Ok(_dbService.GetSlotsforTherapist(id, date));
        }

        

        [ResponseType(typeof(IEnumerable))]
        [Route("api/getslot")]
        public IHttpActionResult GetSlotById(string id)
        {
            return Ok(_dbService.GetSlotById(id));
        }
    }
}

