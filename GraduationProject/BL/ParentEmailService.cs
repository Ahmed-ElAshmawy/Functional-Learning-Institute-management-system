using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduationProject.Models;
using GraduationProject.BL;
using System.Collections;
using AutoMapper;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using NLog;

namespace GraduationProject
{
    public class ParentEmailService : IService<ParentEmailDto>
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public int Delete(string id)
        {
            var email = _db.ParentEmails.FirstOrDefault(a => a.Id == id);

            if (email != null)
            {
                try
                {
                    _db.ParentEmails.Remove(email);
                    _db.SaveChanges();
                    return 1;
                }
                catch(Exception e)
                {
                    Logger.Log(LogLevel.Error, e);

                    return -1;
                }
            }
            return 0;
        }

        public IEnumerable GetAll()
        {
            var emails = _db.ParentEmails.Where(e => e.IsDeleted == false).ToList();
            var emaildto = Mapper.Map<List<ParentEmailDto>>(emails);
            return emaildto;
        }

        public ParentEmailDto GetById(string id)
        {
            var email = _db.ParentEmails.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
            var emailDto = Mapper.Map<ParentEmailDto>(email);
            return emailDto;
        }

        public IEnumerable GetByParent(string parentid)
        {
            var email = _db.ParentEmails.Where(a => a.ParentId==parentid && a.IsDeleted == false);
            var emailDto = Mapper.Map<List<ParentEmailDto>>(email);
            return emailDto;
        }

        public int Update(ParentEmailDto model, string id)
        {
            var email = _db.ParentEmails.FirstOrDefault(a => a.Id == id);
            if (email != null)
            {
                try
                {
                    email.Email = model.Email;
                    email.UpdatedDate = DateTime.Now;
                    email.UpdatedBy = "0000";
                    _db.Entry(email).State = EntityState.Modified;
                    _db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    //change to suitable implementation
                    Logger.Log(LogLevel.Error, e);

                    return -1;
                }
            }
            return 0;
        }
        public int Create(ParentEmailDto model)
        {
            var email = Mapper.Map<ParentEmail>(model);
            try
            {
                email.CreatedBy = "0000";
                _db.ParentEmails.Add(email);
                _db.SaveChanges();
                return 1;
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