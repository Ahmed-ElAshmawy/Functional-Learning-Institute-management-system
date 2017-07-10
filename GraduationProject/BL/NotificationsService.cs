using AutoMapper;
using GraduationProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject
{
    public class NotificationsService
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public IEnumerable GetAll()
        {
            var list = Mapper.Map<List<UserMessageDto>>(_db.UserMessage.ToList());
            return list;
        }
        public UserMessageDto GetById(string id)
        {
            var mes = _db.UserMessage.FirstOrDefault(c => c.Id == id);
            if (mes != null)
            {
                mes.Status = MessageStatus.Read;
                _db.SaveChanges();
            }
            var mesdto = Mapper.Map<UserMessageDto>(mes);

            return mesdto;
        }

    }
}