using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using GraduationProject.Models;
using GraduationProject.Models.dto;
using Microsoft.AspNet.SignalR;
using NLog;
using WebGrease.Css.Extensions;
using static System.Data.Entity.DbFunctions;

namespace GraduationProject.BL
{
    public class SessionService : IService<SessionDto>
    {

        public SessionService()
        {
            _db = new ApplicationDbContext();
            admin = _db.Users.FirstOrDefault(u => u.UserName == "Admin");
        }
        IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalR>();
        ApplicationUser admin;

        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();


        private ApplicationDbContext _db;

        //Count Sessions for a child
        public IEnumerable GetSessionsForChild()
        {
            var children = _db.Children.ToList();
            List<ChildSessions> childSessions = new List<ChildSessions>();
            foreach (var child in children)
            {
                var sessionCount = _db.Sessions.Where(s => s.Date.Month == DateTime.Now.Month
                && s.Date.Year == DateTime.Now.Year
                 && s.Children.Any(ch => ch.Id == child.Id) && s.Status != SessionStatus.Canceled
                 && s.IsDeleted == false).Count();
                childSessions.Add(new ChildSessions { Id = child.Id, Name = child.FirstName + " " + child.LastName, SessionCount = sessionCount });
            }
            return childSessions;
        }
        private void SendNotification(ApplicationUser sender, string FromId, string ToId, string msg, string sessionId)
        {
            var obj = Mapper.Map<UserMessageDto>(new UserMessage { FromId = FromId, MessageBody = msg, Date = DateTime.Now, ToId = ToId, From = sender, Data = sessionId });
            _db.UserMessage.Add(new UserMessage { FromId = FromId, MessageBody = msg, Date = DateTime.Now, ToId = ToId, From = sender, Data = sessionId });
            _db.SaveChanges();
            var conncetionsList = _db.Connections.Where(c => c.UserId == ToId).ToList();
            if (conncetionsList.Any())
            {
                var conncetionsListId = conncetionsList.Select(u => u.ConnectionId).ToArray();
                hubContext.Clients.Clients(conncetionsListId).newMessage(obj);
            }
        }

        /// <summary>
        /// Get all Sessions
        /// </summary>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetAll()
        {
            return Mapper.Map<List<SessionDto>>(_db.Sessions.Where(se => se.IsDeleted == false));
        }

        /// <summary>
        /// Get all Sessions on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetByDate(DateTime date)
        {
            var sessions = _db.Sessions.Where(se => TruncateTime(se.Date) == TruncateTime(date) &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }


        /// <summary>
        /// Get all Sessions on specific date and further dates
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetByRecentDate()
        {
            var sessions = _db.Sessions.Where(se => TruncateTime(se.Date) >= TruncateTime(DateTime.Now) &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }

        /// <summary>
        /// Get all Child session on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="childId">Child Id</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetByDateForChild(DateTime date, string childId)
        {
            var sessions = _db.Sessions.Where(se => TruncateTime(se.Date) == TruncateTime(date) && se.Children.Any(c => c.Id == childId) &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }


        /// <summary>
        /// Get all Child session on this Month
        /// </summary>
        /// <param name="childId">Child Id</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetByDateForChild(string childId)
        {
            var sessions = _db.Sessions.Where(se => se.Date.Month >= DateTime.Now.Month && se.Date.Year >= DateTime.Now.Year && se.Children.Any(c => c.Id == childId) &&
                se.IsDeleted == false).ToList();
            return Mapper.Map<List<SessionDto>>(sessions);
        }




        /// <summary>
        /// Get all therapist session on specific date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="therapistId">therapist Id</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetByDateForTherapist(DateTime date, string therapistId)
        {
            var sessions = _db.Sessions.Where(se => TruncateTime(se.Date) == TruncateTime(date) && se.TherapistId == therapistId &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }

        /// <summary>
        /// Get all therapist session on this month
        /// </summary>
        /// <param name="therapistId">therapist Id</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetByDateForTherapist(string therapistId)
        {
            var sessions = _db.Sessions.Where(se => se.Date.Month >= DateTime.Now.Month && se.Date.Year >= DateTime.Now.Year && se.TherapistId == therapistId &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }

        //show schedule for therapist
        public IEnumerable GetScheduleSessionsTherapist(DateTime date, string id)
        {
            var sessions = _db.Sessions.Where(s => s.Date.Month == date.Month && s.Date.Year == date.Year
              && s.TherapistId == id && s.IsDeleted == false).ToList();
            return Mapper.Map<List<SessionDto>>(sessions);
        }


        public object GetSessionByDate(DateTime date)
        {
            var ch = Mapper.Map<List<TherapistAvailability>>(_db.TherapistAvailability.Where(th => TruncateTime(th.Date) == TruncateTime(date)));
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
            return slotdaDtos;
        }
        //show schedule for child
        public IEnumerable GetScheduleSessionsChild(DateTime date, string id)
        {
            var sessions = _db.Sessions.Where(s => s.Date.Month == date.Month && s.Date.Year == date.Year
            && s.Children.Any(ch => ch.Id == id) && s.IsDeleted == false).ToList();
            return Mapper.Map<List<SessionDto>>(sessions);
        }

        //group session 
        public int CreateGroup(string id, string[] childrenId)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id && se.IsDeleted == false);
            try
            {
                if (session != null)
                {
                    foreach (var item in childrenId)
                    {
                        var child = _db.Children.FirstOrDefault(ch => ch.Id == item && ch.IsDeleted == false);
                        if (child != null)
                        {
                            session.Children.Add(child);
                            foreach (var slot in session.Slots)
                            {
                                _db.ChildrenAvailability.FirstOrDefault(ch => ch.ChildId == item && TruncateTime(ch.Date) == TruncateTime(session.Date) && ch.SlotId == slot.Id).IsBusy = true;

                            }
                        }
                    }
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


        //delete session when childavailability canceled
        public int DeleteSessionByDateAndSlot(DateTime date, string childId, string slotId)
        {
            var child = _db.Children.FirstOrDefault(ch => ch.Id == childId);
            var session = _db.Sessions.FirstOrDefault(se => TruncateTime(se.Date) == TruncateTime(date)
            && se.Children.Any(c => c.Id == childId) && se.Slots.Any(s => s.Id == slotId)
            && se.Status == SessionStatus.Pending &&
                    se.IsDeleted == false);
            try
            {
                if (session != null)
                {
                    //make child free
                    List<string> li = session.Slots.Select(ss => ss.Id).ToList();
                    var temp = _db.ChildrenAvailability.Where(
                        ch =>
                            ch.ChildId == childId && TruncateTime(ch.Date) == TruncateTime(date) &&
                            li.Contains(ch.SlotId)).ToList();
                    temp.ForEach(ar => ar.IsBusy = false);
                    //make child free
                    var therapistAv = session.Therapist.TherapistAvailabilities.Where(
                        th => th.Date.ToShortDateString() == date.ToShortDateString() &&
                            li.Contains(th.SlotId));
                    therapistAv.ForEach(ar => ar.IsBusy = false);

                    if (session.Type == SessionType.Group)
                    {
                        if (child != null)
                        {
                            session.Children.Remove(child);
                        }
                    }
                    else
                    {
                        session.Status = SessionStatus.Canceled;
                        session.IsDeleted = true;
                        session.DeletedDate = DateTime.Now;
                        session.DeletedBy = "0000";
                        _db.Entry(session).State = EntityState.Modified;
                    }


                    var loggedin = _db.Children.FirstOrDefault(c => c.Id == childId).Parent;
                    string msg = loggedin.UserName + " canceled the session on " + date.ToShortDateString();
                    SendNotification(loggedin, loggedin.Id, admin.Id, msg, session.Id);

                    var therapist = session.Therapist;
                    string msg2 = "Your session has been canceld with " + child.FirstName + " " + child.LastName + " on date " + date.ToShortDateString();
                    SendNotification(admin, admin.Id, therapist.Id, msg2, session.Id);

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

        //Assign Therapist for session when therapistavailability canceled
        public int AssignTherapist(DateTime date, string therapistId, string slotId)
        {
            var session = _db.Sessions.FirstOrDefault(se => TruncateTime(se.Date) == TruncateTime(date)
            && se.TherapistId == therapistId && se.Slots.Any(s => s.Id == slotId)
            && se.Status == SessionStatus.Pending &&
                    se.IsDeleted == false);
            try
            {
                if (session != null)
                {
                    List<string> li = session.Slots.Select(ss => ss.Id).ToList();
                    var temp = _db.TherapistAvailability.Where(
                        ch => ch.TherapistId == therapistId && TruncateTime(ch.Date) == TruncateTime(date) &&
                         li.Contains(ch.SlotId)).ToList();
                    temp.ForEach(ar => ar.IsBusy = false);
                    session.Status = SessionStatus.WaitingAdminConfirmation;
                    session.UpdatedDate = DateTime.Now;
                    session.UpdatedBy = "0000";
                    _db.Entry(session).State = EntityState.Modified;


                    var loggedin = _db.Therapists.FirstOrDefault(th => th.Id == therapistId);
                    string msg = loggedin.UserName + " wants to cancel the session on " + date.ToShortDateString();
                    SendNotification(loggedin, loggedin.Id, admin.Id, msg, session.Id);

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
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetDateRange(DateTime from, DateTime to)
        {
            var sessions = _db.Sessions.Where(se => TruncateTime(se.Date) >= TruncateTime(from) &&
            TruncateTime(se.Date) <= TruncateTime(to) &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }
        /// <summary>
        /// Get Child session between two Dates
        /// </summary>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <param name="childId">Child Id</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetDateRangeChild(DateTime from, DateTime to, string childId)
        {
            var sessions = _db.Sessions.
                            Where(se => TruncateTime(se.Date) >= TruncateTime(from) &&
                            TruncateTime(se.Date) <= TruncateTime(to) &&
                            se.Children.Any(c => c.Id == childId) &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }
        /// <summary>
        /// Get Therapist session between two Dates
        /// </summary>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <param name="therapistId">Therapist Id</param>
        /// <returns>IEnumerable Sessions</returns>
        public IEnumerable GetDateRangeTherapist(DateTime from, DateTime to, string therapistId)
        {
            var sessions = _db.Sessions.
                Where(se => TruncateTime(se.Date) >= TruncateTime(from) &&
                TruncateTime(se.Date) <= TruncateTime(to) &&
                se.TherapistId == therapistId &&
                se.IsDeleted == false);
            return Mapper.Map<List<SessionDto>>(sessions);
        }
        /// <summary>
        /// Get Session By Id
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <returns>Session</returns>
        public SessionDto GetById(string id)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id && se.IsDeleted == false);
            return Mapper.Map<SessionDto>(session);
        }

        /// <summary>
        /// Update Session
        /// </summary>
        /// <param name="model">Updated Session</param>
        /// <param name="status">status</param>
        /// <returns></returns>

        public int Update(SessionDto model, bool status)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == model.Id);
            try
            {
                if (session != null)
                {
                    var slots = session.Slots.Select(s => s.Id).ToList();
                    if (session.TherapistId != model.TherapistId)
                    {
                        //make Therapist free
                        if (status)
                        {
                            foreach (var slot in slots)
                            {
                                var availability = session.Therapist.TherapistAvailabilities.FirstOrDefault(
                                      av => (av.Date.ToShortDateString()) == (session.Date.ToShortDateString()) && slot == av.SlotId);
                                if (availability != null)
                                {
                                    availability.IsBusy = false;
                                }
                            }
                            string msg = "Your Session on " + session.Date.ToShortDateString() + " has been canceled you are free now";
                            SendNotification(admin, admin.Id, session.Therapist.Id, msg, session.Id);

                        }
                        //add new therapist availability
                        foreach (var slot in slots)
                        {
                            var therapist = _db.Therapists.FirstOrDefault(t => t.Id == model.TherapistId);

                            if (therapist != null)
                            {
                                var availability = therapist.TherapistAvailabilities.FirstOrDefault(
                                          av => (av.Date.ToShortDateString()) == (session.Date.ToShortDateString()) && slot == av.SlotId);
                                if (availability != null)
                                {
                                    availability.IsBusy = true;
                                }
                            }
                        }
                        session.TherapistId = model.TherapistId;
                        // session.Status = model.Status;
                    }
                    var oldChildren = session.Children.Select(c => c.Id).ToList();
                    var newChildren = model.Children.Select(c => c.Id).ToList();

                    var toadd = newChildren.Except(oldChildren).ToList();
                    var toremove = oldChildren.Except(newChildren).ToList();
                    //toadd
                    foreach (var child in toadd)
                    {
                        session.Children.Add(_db.Children.FirstOrDefault(c => c.Id == child));
                        foreach (var slot in slots)
                        {

                            var childAv =
                                _db.ChildrenAvailability.FirstOrDefault(
                                    cav => cav.ChildId == child && slot == cav.SlotId &&
                                    TruncateTime(cav.Date) == TruncateTime(session.Date));
                            childAv.IsBusy = true;
                        }
                    }

                    //toremove
                    foreach (var child in toremove)
                    {
                        session.Children.Remove(_db.Children.FirstOrDefault(c => c.Id == child));
                        foreach (var slot in slots)
                        {

                            var childAv =
                                _db.ChildrenAvailability.FirstOrDefault(
                                    cav => cav.ChildId == child && slot == cav.SlotId &&
                                    TruncateTime(cav.Date) == TruncateTime(session.Date));
                            childAv.IsBusy = false;
                        }
                    }
                    //Mapper.Map(model, session);
                    session.UpdatedDate = DateTime.Now;
                    session.UpdatedBy = "0000";
                    _db.Entry(session).State = EntityState.Modified;
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
        /// Update Session
        /// </summary>
        /// <param name="model">Updated Session</param>
        /// <returns></returns>
        public int Update(SessionDto model, string id)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id);
            try
            {
                if (session != null)
                {
                    session.TherapistId = model.TherapistId;
                    session.UpdatedDate = DateTime.Now;
                    session.UpdatedBy = "0000";
                    _db.Entry(session).State = EntityState.Modified;
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
        /// Delete session
        /// </summary>
        /// <param name="id">Session ID</param>
        /// <returns></returns>
        public int Delete(string id)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id);
            try
            {
                if (session != null)
                {
                    session.Status = SessionStatus.Canceled;
                    session.IsDeleted = true;
                    session.DeletedDate = DateTime.Now;
                    session.DeletedBy = "0000";
                    _db.Entry(session).State = EntityState.Modified;
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
        /// Cancel Session
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        public int Cancel(string id, bool status)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id);
            try
            {
                if (session != null)
                {
                    var slots = session.Slots.Select(s => s.Id).ToList();
                    var children = session.Children.ToList();
                    var therapist = session.Therapist;
                    session.DeletedBy = therapist.Id;
                    if (status)
                    {
                        //free therapist
                        foreach (var slot in slots)
                        {
                            var availabilities = therapist.TherapistAvailabilities.FirstOrDefault(
                                    av => (av.Date.ToShortDateString()) == (session.Date.ToShortDateString()) && av.SlotId == slot);
                            if (availabilities != null)
                                availabilities.IsBusy = false;
                        }
                        session.DeletedBy = "Admin";
                    }
                    //free all children
                    foreach (var child in children)
                    {
                        foreach (var slot in slots)
                        {
                            var childAvailabitity =
                                child.ChildrenAvailabilities.FirstOrDefault(ca => ca.SlotId == slot &&
                            (ca.Date.ToShortDateString()) == (session.Date.ToShortDateString()));
                            childAvailabitity.IsBusy = false;
                        }
                    }
                    session.Status = SessionStatus.Canceled;
                    session.UpdatedBy = "0000";
                    session.UpdatedDate = DateTime.Now;

                    _db.Entry(session).State = EntityState.Modified;
                    _db.SaveChanges();

                    List<Connection> connectionList = new List<Connection>();



                    foreach (var item in children.Select(i => i.Parent))
                    {
                        var obj = Mapper.Map<UserMessageDto>(new UserMessage { ToId = item.Id, Date = DateTime.Now, MessageBody = "The session on " + session.Date.ToShortDateString() + " has been canceled", FromId = admin.Id, Data = session.Id, From = admin });
                        _db.UserMessage.Add(new UserMessage { ToId = item.Id, Date = DateTime.Now, MessageBody = "The session on " + session.Date.ToShortDateString() + " has been canceled", FromId = admin.Id, Data = session.Id, From = admin });
                        _db.SaveChanges();
                        connectionList.AddRange(_db.Connections.Where(c => c.UserId == item.Id).ToList());
                    }



                    if (connectionList.Any())
                    {
                        var conncetionsListId = connectionList.Select(u => u.ConnectionId).ToArray();
                        hubContext.Clients.Clients(conncetionsListId).newMessage(new { Date = DateTime.Now, MessageBody = "The session on " + session.Date.ToShortDateString() + " has been canceled", FromId = admin.Id, Data = session.Id });
                    }

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
        public int Create(SessionDto model)
        {
            var session = Mapper.Map<Session>(model);
            session.CreatedBy = "0000";
            session.CreatedDate = DateTime.Now;
            session.Status = SessionStatus.Pending;
            try
            {
                _db.Sessions.Add(session);
                foreach (var slot in session.Slots)
                {
                    var therapistAvailability = _db.TherapistAvailability.FirstOrDefault(th => th.TherapistId == model.TherapistId && TruncateTime(th.Date) == TruncateTime(model.Date) && th.SlotId == slot.Id
                    );
                    if (therapistAvailability != null)
                        therapistAvailability.IsBusy = true;
                    _db.Entry(slot).State = EntityState.Unchanged;
                }
                foreach (var child in session.Children)
                {
                    foreach (var slot in session.Slots)
                    {
                        var childAvailability = _db.ChildrenAvailability.FirstOrDefault(ch => ch.ChildId == child.Id && TruncateTime(ch.Date) == TruncateTime(model.Date) && ch.SlotId == slot.Id);
                        if (childAvailability != null)
                            childAvailability.IsBusy = true;
                    }
                    _db.Entry(child).State = EntityState.Unchanged;
                }
                _db.SaveChanges();
                model.Id = session.Id;
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return -1;
            }
        }

        public object GetToUpdate(string id)
        {
            var sessionDetails = GetById(id);
            TherapistAvailabilityService thAvailability = new TherapistAvailabilityService();
            var sessionSlots = sessionDetails.Slots.Select(ss => ss.Id).ToArray();
            var therapistAvailability = thAvailability.GetByAvailabileSlots(sessionDetails.Date, sessionSlots);
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id && se.IsDeleted == false);
            ChildService chAvailability = new ChildService();
            var childAvailability = chAvailability.GetAvailableChildrenForSession(id);
            return new { session = sessionDetails, therapists = therapistAvailability, children = childAvailability };
        }
        public IEnumerable SessionFinshing()
        {
            var sessionFinshing = _db.Sessions.Where(se => se.Date <= DateTime.Now && se.Date.Month >= DateTime.Now.Month && se.IsDeleted == false && se.Status == 0).OrderBy(o => o.Date);
            return Mapper.Map<List<SessionDto>>(sessionFinshing);
        }

        public int ChangeToFinish(string id)
        {
            var session = _db.Sessions.FirstOrDefault(se => se.Id == id);
            try
            {
                if (session != null)
                {
                    session.UpdatedDate = DateTime.Now;
                    session.UpdatedBy = "0000";
                    session.Status = SessionStatus.Finished;
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



        public void Dispose()
        {
            _db.Dispose();
        }
    }
}