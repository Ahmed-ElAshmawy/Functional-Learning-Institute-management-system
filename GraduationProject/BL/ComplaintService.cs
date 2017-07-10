using AutoMapper;
using GraduationProject.Models;
using GraduationProject.Models.dto;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static System.Data.Entity.DbFunctions;

namespace GraduationProject.BL
{
    public class ComplaintService
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalR>();
        ApplicationUser admin;
        public ComplaintService()
        {
            admin = _db.Users.FirstOrDefault(u => u.UserName == "Admin");
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
        public IEnumerable GetAllForUser(string userid)
        {
            var complaintList = _db.Complaints.Where(c =>c.UserId  == userid).ToList();
            return Mapper.Map<List<ComplaintDto>>(complaintList); ;
        }
        public IEnumerable GetAll(string id)
        {
            var mes = _db.UserMessage.FirstOrDefault(c => c.Data == id);
            mes.Status = MessageStatus.Read;
            _db.SaveChanges();
            var complaintList = _db.Complaints.Where(c=>c.Status==ComplaintStatus.Pending).ToList();
            return Mapper.Map<List<ComplaintDto>>(complaintList);
        }
        public ComplaintDto GetComplaint(string id)
        {
            var complaint = _db.Complaints.FirstOrDefault(c => c.Id==id);
            var notification = _db.UserMessage.FirstOrDefault(n => n.Data == id);
            notification.Status = MessageStatus.Read;
            _db.SaveChanges();
            return Mapper.Map<ComplaintDto>(complaint);
        }
        public async Task<int> AddComplaint(ComplaintDto model)
        {
            var complaint = Mapper.Map<Complaint>(model);
            try
            {
                complaint.CreatedDate= DateTime.Now;
                complaint.Status = ComplaintStatus.Pending;
                _db.Complaints.Add(complaint);
                _db.SaveChanges();
                var user = _db.Users.FirstOrDefault(u => u.Id == complaint.UserId);
                await CommonService.SendMail(admin.Email, "New_complaint", "New complaint");
                SendNotification(user, user.Id, admin.Id, "New complaint", complaint.Id);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int DeleteComplaint(string id)
        {
            try
            {
             var complaint=_db.Complaints.FirstOrDefault(a => a.Id == id );
                _db.Complaints.Remove(complaint);
                _db.SaveChanges();
                return 1;
            }
            catch
            {
                return -1;
            }

        }
        public int UpdateComplaint(string id, string answer)
        {
            try
            {
                var complaint = _db.Complaints.FirstOrDefault(c => c.Id == id);
                complaint.Status = ComplaintStatus.Resolved;
                complaint.Answer = answer;
                _db.SaveChanges();
                var user = complaint.User;
                SendNotification(admin, admin.Id, user.Id, "We have reply for your complaint", complaint.Id);
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}