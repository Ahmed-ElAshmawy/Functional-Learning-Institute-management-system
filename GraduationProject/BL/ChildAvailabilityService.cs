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
    public class ChildAvailabilityService : IService<ChildrenAvailabilityDto>
    {
        private ApplicationDbContext _db;
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public ChildAvailabilityService()
        {
            _db = new ApplicationDbContext();
        }
        /// <summary>
        /// Delete ChildrenAvailability
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            var ChildrenAvailability = _db.ChildrenAvailability.FirstOrDefault(a => a.Id == id);
            try
            {
                if (ChildrenAvailability != null)
                {
                    _db.ChildrenAvailability.Remove(ChildrenAvailability);
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
                return -1;
            }
        }
        /// <summary>
        /// Get all ChildrenAvailability
        /// </summary>
        /// <returns></returns>

        public IEnumerable GetAll()
        {
            return Mapper.Map<List<ChildrenAvailabilityDto>>(_db.ChildrenAvailability.OrderBy(o => o.Date).ThenBy(o => o.SlotId));
        }
        /// <summary>
        /// Get all ChildrenAvailability in certain data
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable GetByDate(DateTime date)
        {
            return Mapper.Map<List<ChildrenAvailabilityDto>>(_db.ChildrenAvailability.Where(a => TruncateTime(a.Date) == TruncateTime(date)).OrderBy(o => o.SlotId).ToList());
        }
        /// <summary>
        /// GetByAvailabilityForChild
        /// </summary>
        /// <param name="date"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        public IEnumerable GetByAvailabilityForChild(DateTime date, string childId, bool? IsBusy)
        {
            if (IsBusy == null)
                return Mapper.Map<List<ChildrenAvailabilityDto>>(_db.ChildrenAvailability.Where(a => TruncateTime(a.Date) == TruncateTime(date) && a.ChildId == childId)).OrderBy(o => o.Date).ThenBy(o => o.SlotId).ToList();
            return Mapper.Map<List<ChildrenAvailabilityDto>>(_db.ChildrenAvailability.Where(a => TruncateTime(a.Date) == TruncateTime(date) && a.ChildId == childId && a.IsBusy == IsBusy)).OrderBy(o => o.Date).ThenBy(o => o.SlotId).ToList();
        }
        /// <summary>
        /// GetByAvailabilityForChild
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public IEnumerable GetAvailabilityForChildById(string childId)
        {
            var ChildrenAvailability = _db.ChildrenAvailability.Where(a => a.ChildId == childId && a.Date.Month >= DateTime.Now.Month && a.Date.Year >= DateTime.Now.Year).OrderBy(o => o.Date).ThenBy(o => o.SlotId).ToList();
            return Mapper.Map<List<ChildrenAvailabilityDto>>(ChildrenAvailability);
        }

        /// <summary>
        /// Get certain ChildrenAvailability by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ChildrenAvailabilityDto GetById(string id)
        {
            return Mapper.Map<ChildrenAvailabilityDto>(_db.ChildrenAvailability.FirstOrDefault(a => a.Id == id));
        }
        /// <summary>
        /// Update ChildrenAvailability
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Update(ChildrenAvailabilityDto model, string id)
        {
            var ChildrenAvailability = _db.ChildrenAvailability.FirstOrDefault(a => a.Id == id);
            try
            {
                if (ChildrenAvailability != null)
                {
                    Mapper.Map(model, ChildrenAvailability);
                    _db.Entry(ChildrenAvailability).State = EntityState.Modified;
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
                return -1;
            }
        }
        /// <summary>
        /// Update ChildrenAvailability state
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateState(string id, bool IsBusy)
        {
            var ChildrenAvailability = _db.ChildrenAvailability.FirstOrDefault(a => a.Id == id);
            try
            {
                if (ChildrenAvailability != null)
                {
                    ChildrenAvailability.IsBusy = IsBusy;
                    _db.Entry(ChildrenAvailability).State = EntityState.Modified;
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
                return -1;
            }
        }
        /// <summary>
        /// Create ChildrenAvailability
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Create(ChildrenAvailabilityDto model)
        {
            ChildrenAvailability childrenAvailability = Mapper.Map<ChildrenAvailability>(model);
            try
            {
                _db.ChildrenAvailability.Add(childrenAvailability);
                _db.SaveChanges();
                model.Id = childrenAvailability.Id;
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int CreateAvailability(CreateChildrenAvailabilityDto model)
        {
            bool Added = false;
            try
            {     
                if(model.Date>= DateTime.Now)
                {
                    var childAvail = _db.ChildrenAvailability.Where(d => d.Date == model.Date&&d.ChildId==model.ChildId).Select(s => s.SlotId).ToList();
                    if (childAvail.Count() == 0)
                    {
                        foreach (var slot in model.Slots)
                        {
                            ChildrenAvailability ChildrenAvailability = new ChildrenAvailability();
                            ChildrenAvailability.ChildId = model.ChildId;
                            ChildrenAvailability.Date = model.Date;
                            ChildrenAvailability.Location = model.Location;
                            ChildrenAvailability.IsBusy = false;
                            ChildrenAvailability.SlotId = slot;
                            _db.ChildrenAvailability.Add(ChildrenAvailability);
                        }
                        _db.SaveChanges();
                        return 1;
                     }

                     foreach (var slot in model.Slots)
                        {
                           if (!childAvail.Contains(slot))
                             {
                                 ChildrenAvailability ChildrenAvailability = new ChildrenAvailability();
                                 ChildrenAvailability.ChildId = model.ChildId;
                                 ChildrenAvailability.Date = model.Date;
                                 ChildrenAvailability.Location = model.Location;
                                 ChildrenAvailability.IsBusy = false;
                                 ChildrenAvailability.SlotId = slot;
                                _db.ChildrenAvailability.Add(ChildrenAvailability);
                                _db.SaveChanges();
                                Added = true;
                              }
                          }
                      if(Added)
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
                return -1;
            }
        }
        public void Dispose()
        {
            _db.Dispose();
        }        
    }
}