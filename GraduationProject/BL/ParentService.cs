using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using AutoMapper;
using GraduationProject.BL;
using GraduationProject.Models;
using Microsoft.AspNet.Identity;
using NLog;

namespace GraduationProject
{
    public class ParentService : IService<BaseParentDto>
    {
        private ApplicationDbContext _db;
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public ParentService()
        {
            _db = new ApplicationDbContext();
        }


        /// <summary>
        /// Get All Parents
        /// </summary>
        /// <returns>List of ParentDto</returns>
        public IEnumerable GetAll()
        {
            return Mapper.Map<List<ParentDto>>(_db.Parents.Where(p => p.IsDeleted == false));
        }

        /// <summary>
        /// Get parent By Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>ParentDto</returns>
        public BaseParentDto GetById(string id)
        {
            return Mapper.Map<ParentDto>(_db.Parents.FirstOrDefault(a => a.Id == id &&a.IsDeleted==false));
        }


        /// <summary>
        /// Update Parent
        /// </summary>
        /// <param name="id">Parent Id</param>
        /// <param name="model">Model</param>
        /// <returns>0 notfound</returns>
        /// <returns>1 updated</returns>
        /// <returns>-1 error</returns>
        public int Update(BaseParentDto model, string id)
        {
            var parent = _db.Parents.Find(model.Id);
            if (parent != null)
            {
                try
                {
                    if (_db.Users.Any(u => u.Email == model.Email && u.Id != model.Id))
                    {
                        return 2;
                    }
                    parent.BirthDate = model.BirthDate;
                    parent.Email = model.Email;
                    parent.City = model.City;
                    parent.FatherName = model.FatherName;
                    parent.MotherName = model.MotherName;
                    parent.PostalCode = model.PostalCode;
                    parent.Province = model.Province;
                    parent.StreetName = model.StreetName;
                    parent.StreetNo = model.StreetNo;
                    parent.UpdatedDate = DateTime.Now;
                    parent.UpdatedBy = "0000";
                    _db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, e);

                    //log Exception
                    return -1;
                }
            }
            else
                return 0;
        }

        /// <summary>
        /// Delete Parent By Id
        /// </summary>
        /// <param name="id">Parent Id</param>
        /// <returns>0 notfound</returns>
        /// <returns>1 deleted</returns>
        /// <returns>-1 error</returns>
        public int Delete(string id)
        {
            var parent = _db.Parents.Find(id);
            if (parent != null)
            {
                try
                {
                    parent.IsDeleted = true;
                    parent.DeletedDate = DateTime.Now;
                    parent.DeletedBy = "0000";
                    _db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, e);

                    //log Exception
                    return -1;
                }
            }
            else
                return 0;
        }

        /// <summary>
        /// Create Parent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Create(CreateParentDto model)
        {
            try
            {
                string password = Membership.GeneratePassword(8, 2);
                Parent parent = Mapper.Map<Parent>(model);
                List<Contact> parentContacts = new List<Contact>();
                parentContacts.AddRange(parent.Contacts.ToArray());
                parent.Emails.Clear();
                parent.Contacts.Clear();
                parent.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
               // parent.CreatedBy = "0000";
                IdentityResult res = await CommonService.CreateUser(parent, password);               
                if (res.Succeeded)
                {
                    foreach (var contact in parentContacts)
                    {
                        //contact.CreatedBy = "0000";
                        contact.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
                        contact.ParentId = parent.Id;
                        _db.Contacts.Add(contact);
                    }
                    _db.SaveChanges();
                    await CommonService.AddToRole("Parent", parent.Id);
                    var body = "<p>Hi {0} <p>Wellcome to our web site </p> <p>Username: {0}</p><p>Password : {1}</p><p>Thanks for using our services</p>";
                    int sended = await CommonService.SendMail(parent.Email, "Wellcome", string.Format(body, parent.UserName, password));
                    model.Id = parent.Id;
                    if (sended != -1)
                        return 1;
                }
                return -1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                //log Exception              
                return -1;
            }
        }


        public IEnumerable GetParentChild(string id)
        {
            return Mapper.Map<List<ChildDto>>(_db.Children.Where(p => p.ParentId == id && p.IsDeleted == false));
        }

        public List<ParentDto> RetrieveList()
        {
            var Children = _db.Parents.Where(a => a.IsDeleted == true).ToList();
            var parentDto = Mapper.Map<List<ParentDto>>(Children);
            return parentDto;
        }

        public int RetrieveParent(string id)
        {
            var parent = _db.Parents.Find(id);
            if (parent != null)
            {
                try
                {
                    parent.IsDeleted = false;
                    _db.SaveChanges();
                    return 1;
                }
                catch (Exception e)
                {
                    //log Exception
                    Logger.Log(LogLevel.Error, e);
                    return -1;
                }
            }
            else
                return 0;
        }


        public void Dispose()
        {
            _db.Dispose();
        }

    }
}