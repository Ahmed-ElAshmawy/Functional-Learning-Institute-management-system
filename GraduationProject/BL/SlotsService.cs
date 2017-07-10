using AutoMapper;
using GraduationProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace GraduationProject.BL
{
    public class SlotsService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public IEnumerable GetAll()
        {
            return Mapper.Map<List<SlotDto>>(_db.Slots);
        }


        public SlotDto GetSlotById(string id)
        {
            var result = _db.Getsolts(id);            
            return Mapper.Map<SlotDto>(result);
        }

        public IEnumerable GetSlotsforChild(string id,DateTime date)
        {
            var childSlots = _db.ChildrenAvailability.Where(ch => ch.ChildId == id&&ch.Date==date).Select(s=>s.Slot).ToList();
            var dbslots = _db.Slots.ToList();
            var slots = dbslots.Except(childSlots).ToList();
            return Mapper.Map<List<SlotDto>>(slots);
        }

        public IEnumerable GetSlotsforTherapist(string id, DateTime date)
        {
            var therapistSlots = _db.TherapistAvailability.Where(ch => ch.TherapistId == id && ch.Date == date).Select(s => s.Slot).ToList();
            var dbslots = _db.Slots.ToList();
            var slots = dbslots.Except(therapistSlots).ToList();
            return Mapper.Map<List<SlotDto>>(slots);
        }


    }
}