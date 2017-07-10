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
    public class ChildrenAvailabilityController : ApiController
    {
        private ChildAvailabilityService _dbService;

        public ChildrenAvailabilityController()
        {
            _dbService = new ChildAvailabilityService();
        }
        // GET: api/ChildrenAvailability
        [Route("api/ChildrenAvailability")]
        public IHttpActionResult GetChildrenAvailability()
        {
            return Ok(_dbService.GetAll());
        }
        /// <summary>
        /// Get All ChildrenAvailability on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/ChildrenAvailability/GetByDate")]
        public IEnumerable GetChildrenAvailabilityByDate(DateTime date)
        {
            return _dbService.GetByDate(date);
        }
        /// <summary>
        /// GetByAvailabilityForChild
        /// </summary>
        /// <param name="date"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/ChildrenAvailability/GetDateAndId")]
        public SlotsIdDto GetByAvailabilityForChild(string childId, DateTime date, bool? IsBusy)
        {
            var ch = _dbService.GetByAvailabilityForChild(date, childId, IsBusy);
            SlotsIdDto slotdaDto = new SlotsIdDto();
            slotdaDto.Id = childId;
            slotdaDto.Slots = ((List<ChildrenAvailabilityDto>)ch).Select(x => x.SlotId).ToList();
            return slotdaDto;
        }

        /// <summary>
        /// GetByAvailabilityForChild
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable))]
        [Route("api/ChildrenAvailability/GetAvailabilityForChildById")]
        public IEnumerable GetAvailabilityForChildById(string id)
        {
            return _dbService.GetAvailabilityForChildById(id);
        }

        /// <summary>
        /// Update ChildrenAvailability
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        // PUT: api/ChildrenAvailability/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChildrenAvailability(string id, ChildrenAvailabilityDto childrenAvailabilityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _dbService.Update(childrenAvailabilityDto, id);
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
        public IHttpActionResult PutChildrenAvailabilityState(string id, bool IsBusy)
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


        [ResponseType(typeof(ChildrenAvailability))]
        [Route("api/ChildrenAvailability/Post")]
        public IHttpActionResult PostChildrenAvailabilityDto(CreateChildrenAvailabilityDto model)
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

        // DELETE: api/ChildrenAvailability/5
        [ResponseType(typeof(ChildrenAvailability))]
        public IHttpActionResult DeleteChildrenAvailability(string id)
        {
            var result = _dbService.Delete(id);
            if (result == 0)
                return NotFound();
            else if (result == -1)
                return BadRequest();
            return Ok(new { Message = "Deleted" });
        }
        // GET: api/ChildrenAvailability/5
        [ResponseType(typeof(ChildrenAvailability))]
        [Route("api/ChildrenAvailability/GetById")]
        public IHttpActionResult GetChildrenAvailabilityById(string id)
        {
            ChildrenAvailabilityDto childrenAvailabilityDto = _dbService.GetById(id);
            if (childrenAvailabilityDto == null)
            {
                return NotFound();
            }
            return Ok(childrenAvailabilityDto);
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
