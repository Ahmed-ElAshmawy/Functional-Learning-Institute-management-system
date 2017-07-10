using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GraduationProject.Models;
using System.Data.Entity;
using GraduationProject.BL;
using System.Collections;
using GraduationProject.Models.dto;
using Microsoft.AspNet.Identity;
using NLog;
using static System.Data.Entity.DbFunctions;


namespace GraduationProject
{
    public class ChildService : IService<ChildDto>
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public int create(ChildDto model)
        {
            model.Id = Guid.NewGuid().ToString();
            Child child = Mapper.Map<Child>(model);
            child.CreatedBy = "0000";
            try
            {
                _db.Children.Add(child);
                ClientFund clienfund = new ClientFund
                {
                    BillingNumber = model.BillingNumber,
                    Amount = model.Amount,
                    ChildId = child.Id,
                    EndTime = model.EndTime,
                    Remaining = model.Remaining,
                    StartTime = model.StartTime,
                    Used = model.Used
                };
                _db.ClientFund.Add(clienfund);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return 0;
            }

        }

        public int Delete(string id)
        {
            try
            {
                Child child = _db.Children.FirstOrDefault(ch => ch.Id == id);
                child.IsDeleted = true;
                child.DeletedDate = DateTime.Now;
                child.DeletedBy = "0000";
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);

                return 0;
            }
        }

        public IEnumerable GetAll()
        {
            var list = Mapper.Map<List<ChildDto>>(_db.Children.Where(ch => ch.IsDeleted == false).ToList());
            return list;
        }


        //children have the same slots in specific session
        public IEnumerable GetAvailableChildren(string id)
        {
            var session = _db.Sessions.FirstOrDefault(s => s.Id == id && s.IsDeleted == false && s.Status == SessionStatus.Pending);
            var date = session.Date;
            var slots = session.Slots.Select(s => s.Id).ToList();//slots in the session
            List<Slotsdto> temp = new List<Slotsdto>();
            var childAvailability = _db.ChildrenAvailability.Where(ch => TruncateTime(ch.Date) == TruncateTime(date)
              && ch.IsBusy == false).ToList();
            foreach (var item in childAvailability)
            {
                var exist = temp.FirstOrDefault(e => e.Id == item.ChildId);
                if (exist == null)
                {
                    var childSlots = new Slotsdto { Id = item.ChildId };
                    childSlots.Slots.Add(item.SlotId);
                    temp.Add(childSlots);
                }
                else
                {
                    exist.Slots.Add(item.SlotId);
                }
            }
            List<GList> children = new List<GList>();
            foreach (var item in temp)
            {
                bool s = slots.Except(item.Slots).Any();
                if (!s)
                {
                    var child = _db.Children.FirstOrDefault(c => c.Id == item.Id);
                    if (child != null)
                        children.Add(new GList { Id = item.Id, Value = child.FirstName + " " + child.LastName });
                }
            }
            return children;
        }

        //children have the same slots in specific session
        public IEnumerable GetAvailableChildrenForSession(string id)
        {
            var session = _db.Sessions.FirstOrDefault(s => s.Id == id);
            var slots = session.Slots.Select(s => s.Id).ToList();
            var av = Mapper.Map<List<ChildrenAvailabilityDto>>(_db.ChildrenAvailability.
               Where(a => TruncateTime(a.Date) == TruncateTime(session.Date) && a.IsBusy == false).ToList());
            List<Slotsdto> temp = new List<Slotsdto>();
            foreach (var item in av)
            {
                var t = temp.FirstOrDefault(x => x.Id == item.ChildId);
                if (t == null)
                {
                    var th = new Slotsdto { Id = item.ChildId };
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
                bool y = slots.Except(item.Slots).Any();
                if (!y)
                {
                    var child = _db.Children.FirstOrDefault(s => s.Id == item.Id);
                    if (child != null)
                        thList.Add(new GList { Id = child.Id, Value = child.FirstName + " " + child.LastName });
                }
            }
            return thList;
        }


        public ChildDto GetById(string id)
        {
            var ch = _db.Children.FirstOrDefault(c => c.Id == id && c.IsDeleted == false);
            var child = Mapper.Map<ChildDto>(ch);
            child.BillingNumber = ch.ClientFunds.OrderBy(x => x.EndTime).Last().BillingNumber;
            child.Amount = ch.ClientFunds.OrderBy(x => x.EndTime).Last().Amount;
            child.EndTime = ch.ClientFunds.OrderBy(x => x.EndTime).Last().EndTime;
            child.StartTime = ch.ClientFunds.OrderBy(x => x.EndTime).Last().StartTime;
            return child;
        }

        public int Update(ChildDto model, string id)
        {
            var Originalchild = _db.Children.FirstOrDefault(ch => ch.Id == id);
            if (Originalchild != null)
            {
                try
                {
                    Originalchild.BirthDate = model.BirthDate;
                    Originalchild.ClientSince = model.ClientSince;
                    Originalchild.FirstName = model.FirstName;
                    Originalchild.FundGroup = model.FundGroup;
                    Originalchild.Initials = model.Initials;
                    Originalchild.LastName = model.LastName;
                    Originalchild.MedicalProvider = model.MedicalProvider;
                    Originalchild.MedicalProviderPhone = model.MedicalProviderPhone;
                    Originalchild.ParentId = model.ParentId;
                    Originalchild.SOLocation = model.SOLocation;
                    Originalchild.UpdatedBy = "0000";
                    Originalchild.UpdatedDate = DateTime.Now;

                    _db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, e);

                    return 0;
                }
            }
            return 0;

        }


        //test
        public int RetrieveChild(string id)
        {
            try
            {
                Child child = _db.Children.FirstOrDefault(ch => ch.Id == id);
                child.IsDeleted = false;
                child.DeletedDate = DateTime.Now;
                child.DeletedBy = "0000";
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);

                return 0;
            }
        }
        public List<ChildDto> RetrieveList()
        {
            var Children = _db.Children.Where(a => a.IsDeleted == true).ToList();
            var ChildrenDto = Mapper.Map<List<ChildDto>>(Children);
            return ChildrenDto;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}