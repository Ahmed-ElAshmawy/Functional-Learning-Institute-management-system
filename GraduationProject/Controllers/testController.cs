using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GraduationProject.Models;
using static System.Data.Entity.DbFunctions;

namespace GraduationProject.Controllers
{
    public class testController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public object getall()
        {
            ////getchild
            //var ch = Mapper.Map<List<ChildrenAvailabilityDto>>(db.ChildrenAvailability);
            //List<slotsIdDto> slotdaDtos = new List<slotsIdDto>();
            //var ids = ch.Select(c => c.ChildId).Distinct().ToList();
            //foreach (string Id in ids)
            //{
            //    slotsIdDto temp = new slotsIdDto();
            //    var yy = ch.Where(ch1 => ch1.ChildId == Id);
            //    temp.Id = Id;
            //    temp.Slots = yy.Select(y => y.SlotId).OrderBy(xr => xr).ToList();
            //    slotdaDtos.Add(temp);
            //}
            //return Ok(slotdaDtos);
            //getchild

            var ch = Mapper.Map<List<TherapistAvailability>>(db.TherapistAvailability);
            List<SlotsIdDto> slotdaDtos = new List<SlotsIdDto>();
            var ids = ch.Select(c => new { Id = c.TherapistId, name = c.Therapist.FullLegalName }).Distinct().ToList();
            foreach (var item in ids)
            {
                SlotsIdDto temp = new SlotsIdDto();
                var yy = ch.Where(ch1 => ch1.TherapistId == item.Id);
                temp.Name = item.name;
                temp.Id = item.Id;
                temp.Slots = yy.Select(y => y.SlotId).OrderBy(xr => xr).ToList();
                slotdaDtos.Add(temp);
            }
            return Ok(slotdaDtos);
        }
        [Route("api/test/getby")]
        public object getbydate(DateTime date)
        {
            var ch = Mapper.Map<List<TherapistAvailability>>(db.TherapistAvailability.Where(th=> TruncateTime(th.Date)== TruncateTime(date)));
            List<SlotsIdDto> slotdaDtos = new List<SlotsIdDto>();
            var ids = ch.Select(c => new { Id = c.TherapistId, name = c.Therapist.FullLegalName }).Distinct().ToList();
            foreach (var item in ids)
            {
                SlotsIdDto temp = new SlotsIdDto();
                var yy = ch.Where(ch1 => ch1.TherapistId == item.Id);
                temp.Name = item.name;
                temp.Id = item.Id;
                temp.Slots = yy.Select(y => y.SlotId).OrderBy(xr => xr).ToList();
                slotdaDtos.Add(temp);
            }
            return Ok(slotdaDtos);
        }
    }
}
