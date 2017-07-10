using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GraduationProject.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutoMapper;

namespace GraduationProject
{
    public class SignalR : Hub
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public void sendMessage(string mesg)
        {
            var connectionId = Context.ConnectionId;
            var connectedUser = _db.Connections.FirstOrDefault(x => x.ConnectionId == connectionId);
            var dbuser = _db.Users.Find(connectedUser.UserId);
            var adminId = _db.Users.FirstOrDefault(u => u.UserName == "Admin").Id;

            var conncetionsList = _db.Connections.Where(c => c.UserId == adminId).ToList();
            if (conncetionsList.Any())
            {
                var conncetionsListId = conncetionsList.Select(u => u.ConnectionId).ToArray();
                var obj = new UserMessage { FromId = dbuser.Id, MessageBody = mesg, Date = DateTime.Now, ToId = adminId };
                _db.UserMessage.Add(obj);
                _db.SaveChanges();

                Clients.Clients(conncetionsListId).newMessage(new { sender = dbuser.UserName, msg = obj.MessageBody,id=obj.Id,date=obj.Date });
            }
        }

        public void Wellcome()
        {
            Clients.Client(Context.ConnectionId).sync("");

        }

        public void NewConnection(string userId)
        {
            var id = userId;
            var connectionID = Context.ConnectionId;
            _db.Connections.Add(new Connection { ConnectionId = connectionID, UserId = id });
            _db.SaveChanges();
            var currentUser = _db.Users.FirstOrDefault(u => u.Id == userId);
            //send user unreaded msg
            //var messeges = currentUser.UserMessage.Where(m => m.Status == MessageStatus.UnRead);
            var messeges =_db.UserMessage.Where(m => m.Status == MessageStatus.UnRead && m.ToId==id).ToList();
            var list = Mapper.Map<List<UserMessageDto>>(messeges);
            if (messeges.Count() > 0)
                Clients.Client(Context.ConnectionId).sync(list);
        }

        public void ToClient(string sessionId,string fromId,string toId,string mesg)
        {
            var session = _db.Sessions.FirstOrDefault(s => s.Id == sessionId);
            var date = session.Date;
            var child = session.Children.First();
            var therapist = session.Therapist;
            var conncetionsList = _db.Connections.Where(c => c.UserId == therapist.Id).ToList();

            var admin = _db.Users.FirstOrDefault(u => u.UserName == "Admin");

            if (conncetionsList.Any())
            {
                var conncetionsListId = conncetionsList.Select(u => u.ConnectionId).ToArray();
                var obj = Mapper.Map < UserMessageDto > (new UserMessage { FromId = admin.Id, MessageBody = "You session has been canceld with "+child.FirstName+" "+child.LastName+" on date "+date, Date = DateTime.Now, ToId = therapist.Id,From= admin,Data= sessionId });
                _db.UserMessage.Add(new UserMessage { FromId = admin.Id, MessageBody = "Your session has been canceld with " + child.FirstName + " " + child.LastName + " on date " + date, Date = DateTime.Now, ToId = therapist.Id, Data = sessionId });
                _db.SaveChanges();
                Clients.Clients(conncetionsListId).confirmClient(obj);
            }
        }

        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    var connectionID = Context.ConnectionId;
        //    _db.Connections.Remove(_db.Connections.FirstOrDefault(c => c.ConnectionId == connectionID));
        //    _db.SaveChanges();
        //    return base.OnDisconnected(stopCalled);
        //}
    }
}