using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using AutoMapper;
using GraduationProject.Models;
using GraduationProject.Models.dto;
using System.Data.Entity;
using NLog;
using static System.Data.Entity.DbFunctions;


namespace GraduationProject.BL
{
    public class TherapistAvailabilityService : IService<TherapistAvailabilityDto>
    {
        private ApplicationDbContext _db;
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        public TherapistAvailabilityService()
        {
            _db = new ApplicationDbContext();
        }
        /// <summary>
        /// Delete TherapistAvailability
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            var TherapistAvailability = _db.TherapistAvailability.FirstOrDefault(a => a.Id == id);
            try
            {
                if (TherapistAvailability != null)
                {
                    _db.TherapistAvailability.Remove(TherapistAvailability);
                    _db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);

                return -1;
            }
        }
        /// <summary>
        /// Get all TherapistAvailability
        /// </summary>
        /// <returns></returns>

        public IEnumerable GetAll()
        {
            return Mapper.Map<List<TherapistAvailabilityDto>>(_db.TherapistAvailability.OrderBy(o => o.Date).ThenBy(o => o.SlotId));
        }

        /// <summary>
        /// Get all TherapistAvailability in certain data
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable GetByDate(DateTime date)
        {
            return Mapper.Map<List<TherapistAvailabilityDto>>(_db.TherapistAvailability.Where(a => TruncateTime(a.Date) == TruncateTime(date)).OrderBy(o => o.SlotId).ToList());
        }

        /// <summary>
        /// Get all TherapistAvailability in certain data
        /// </summary>
        /// <param name="date"></param>
        /// <param name="slots"></param>
        /// <returns></returns>
        public IEnumerable GetByAvailabileSlots(DateTime date, string[] slots)
        {
            var av = Mapper.Map<List<TherapistAvailabilityDto>>(_db.TherapistAvailability.
                Where(a => TruncateTime(a.Date) == TruncateTime(date)&&a.IsBusy==false).ToList());
            List <Slotsdto> temp= new List<Slotsdto>();
            foreach (var item in av)
            {
                var t = temp.FirstOrDefault(x => x.Id == item.TherapistId);
                if (t == null)
                {
                    var th = new Slotsdto {Id = item.TherapistId};
                    th.Slots.Add(item.SlotId);
                    temp.Add(th);
                }
                else
                {
                    t.Slots.Add(item.SlotId);
                }
            }
            var thList = new List<GList>();
            foreach (var item in temp)
            {
                bool y= slots.Except(item.Slots).Any();
                if (!y)
                {
                    var therapist = _db.Therapists.FirstOrDefault(s=>s.Id==item.Id);
                    if (therapist != null)
                        thList.Add(new GList {Id = therapist.Id,Value = therapist.FullLegalName });
                }
            }
              return thList;
        }





        /// <summary>
        /// GetTherapistAvailability
        /// </summary>
        /// <param name="date"></param>
        /// <param name="TherapistId"></param>
        /// <returns></returns>
        public IEnumerable GetByTherapistAvailability(DateTime date, string TherapistId, bool? IsBusy)
        {
            if (IsBusy == null)
                return Mapper.Map<List<TherapistAvailabilityDto>>(_db.TherapistAvailability.Where(a => TruncateTime(a.Date) == TruncateTime(date) && a.TherapistId == TherapistId).OrderBy(o => o.Date).ThenBy(o => o.SlotId).ToList());
            return Mapper.Map<List<TherapistAvailabilityDto>>(_db.TherapistAvailability.Where(a => TruncateTime(a.Date) == TruncateTime(date) && a.TherapistId == TherapistId && a.IsBusy == IsBusy).OrderBy(o => o.Date).ThenBy(o => o.SlotId).ToList());
        }
        /// <summary>
        /// GetTherapistAvailability
        /// </summary>
        /// <param name="TherapistId"></param>
        /// <returns></returns>
        public IEnumerable GetTherapistAvailabilityById(string TherapistId)
        {
            var TherapistAvailability = _db.TherapistAvailability.Where(a => a.TherapistId == TherapistId && a.Date.Month>= DateTime.Now.Month && a.Date.Year>= DateTime.Now.Year).OrderBy(o=>o.Date).ThenBy(o=>o.SlotId).ToList();
            return Mapper.Map<List<TherapistAvailabilityDto>>(TherapistAvailability);
        }
        /// <summary>
        /// Get certain TherapistAvailability by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TherapistAvailabilityDto GetById(string id)
        {
            return Mapper.Map<TherapistAvailabilityDto>(_db.TherapistAvailability.FirstOrDefault(a => a.Id == id));
        }

        /// <summary>
        /// Update TherapistAvailability
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Update(TherapistAvailabilityDto model, string id)
        {
            var TherapistAvailability = _db.TherapistAvailability.FirstOrDefault(a => a.Id == id);
            try
            {
                if (TherapistAvailability != null)
                {
                    Mapper.Map(model, TherapistAvailability);
                    _db.Entry(TherapistAvailability).State = EntityState.Modified;
                    _db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return -1;
            }
        }
        /// <summary>
        /// Update TherapistAvailability state
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateState(string id, bool IsBusy)
        {
            var TherapistAvailability = _db.TherapistAvailability.FirstOrDefault(a => a.Id == id);
            try
            {
                if (TherapistAvailability != null)
                {
                    TherapistAvailability.IsBusy = IsBusy;
                    _db.Entry(TherapistAvailability).State = EntityState.Modified;
                    _db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return -1;
            }
        } /// <summary>
          /// Create TherapistAvailability
          /// </summary>
          /// <param name="model"></param>
          /// <returns></returns>
        public int Create(TherapistAvailabilityDto model)
        {
            TherapistAvailability therapistAvailability = Mapper.Map<TherapistAvailability>(model);
            try
            {
                _db.TherapistAvailability.Add(therapistAvailability);
                _db.SaveChanges();
                model.Id = therapistAvailability.Id;
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int CreateAvailability(CreateTherapistAvailabilityDto model)
        {
            bool Added = false;
            try
            {
                if (model.Date >= DateTime.Now)
                {
                    var therapisAvail = _db.TherapistAvailability.Where(d => d.Date == model.Date && d.TherapistId == model.TherapistId).Select(s => s.SlotId).ToList();
                    if (therapisAvail.Count() == 0)
                    {
                        foreach (var slot in model.Slots)
                        {
                            TherapistAvailability TherapistAvailability = new TherapistAvailability();
                            TherapistAvailability.TherapistId = model.TherapistId;
                            TherapistAvailability.Date = model.Date;
                            TherapistAvailability.IsBusy = false;
                            TherapistAvailability.SlotId = slot;
                            _db.TherapistAvailability.Add(TherapistAvailability);
                            model.Id = TherapistAvailability.Id;
                        }
                        _db.SaveChanges();
                        return 1;
                    }

                    foreach (var slot in model.Slots)
                    {
                        if (!therapisAvail.Contains(slot))
                        {
                            TherapistAvailability TherapistAvailability = new TherapistAvailability();
                            TherapistAvailability.TherapistId = model.TherapistId;
                            TherapistAvailability.Date = model.Date;
                            TherapistAvailability.IsBusy = false;
                            TherapistAvailability.SlotId = slot;
                            _db.TherapistAvailability.Add(TherapistAvailability);
                            _db.SaveChanges();
                            Added = true;
                        }
                    }
                    if (Added)
                        return 1;
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);

                return -1;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}