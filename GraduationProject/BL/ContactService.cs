using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using GraduationProject.Models;
using Microsoft.AspNet.Identity;
using NLog;

namespace GraduationProject.BL
{
    public class ContactService : IService<ContactDto>
    {
        public ContactService()
        {
            _db = new ApplicationDbContext();
        }
        private ApplicationDbContext _db;
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public IEnumerable GetAll()
        {            
            return Mapper.Map<List<ContactDto>>(_db.Contacts.Where(co => co.IsDeleted == false));
        }

        public ContactDto GetById(string id)
        {
            return Mapper.Map<ContactDto>(_db.Contacts.FirstOrDefault(a => a.Id == id && a.IsDeleted == false));
        }

        public IEnumerable GetByParentId(string id)
        {
            return Mapper.Map<List<ContactDto>>(_db.Contacts.Where(a => a.ParentId == id && a.IsDeleted == false));
        }

        public int Update(ContactDto model, string id)
        {
            var contact = _db.Contacts.FirstOrDefault(co => co.Id == id);
            if (contact != null)
            {
                contact.UpdatedDate = DateTime.Now;
                contact.UpdatedBy = "0000";
                // contact.Number = model.ContactName;
                // contact.ContactType = model.ContactType;
                // _db.Entry(contact).State = EntityState.Modified;
                contact.Number = model.Number;
                contact.ContactType = model.ContactType;
                contact.ContactName = model.ContactName;
                _db.SaveChanges();
                return 1;

            }
            return -1;
        }

        public int Create(ContactDto model)
        {
            var contact = Mapper.Map<Contact>(model);
            try
            {
                contact.CreatedBy = "0000";
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                model.Id = contact.Id;
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return -1;
            }
        }

        public int Delete(string id)
        {
            var contact = _db.Contacts.FirstOrDefault(a => a.Id == id);
            if (contact != null)
            {
                _db.Contacts.Remove(contact);
                _db.SaveChanges();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int RetrieveContact(string id)
        {
            var contact = _db.Contacts.FirstOrDefault(a => a.Id == id);
            if (contact != null)
            {
                contact.IsDeleted = false; ;
                _db.SaveChanges();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public List<ContactDto> RetrieveList()
        {
            var Contact = _db.Contacts.Where(a => a.IsDeleted == true).ToList();
            var contactDto = Mapper.Map<List<ContactDto>>(Contact);
            return contactDto;
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}