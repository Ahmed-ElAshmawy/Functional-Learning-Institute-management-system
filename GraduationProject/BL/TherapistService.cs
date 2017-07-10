using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduationProject.Models;
using AutoMapper;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Security;
using System.Collections;
using GraduationProject.BL;
using NLog;

namespace GraduationProject
{
    public class TherapistService : IService<BaseTherapistDto>
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            var therapist = _db.Therapists.FirstOrDefault(a => a.Id == id);
            if (therapist != null)
            {
                try
                {
                    therapist.IsDeleted = true;
                    therapist.DeletedDate = DateTime.Now;
                    therapist.DeletedBy = "0000";
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
            var therapistList = _db.Therapists.Where(th => th.IsDeleted == false);
            var therapistListdto = Mapper.Map<List<TherapistDto>>(therapistList);
            return therapistListdto;
        }

        public BaseTherapistDto GetById(string id)
        {
            var therapis = _db.Therapists.FirstOrDefault(a => a.Id == id && a.IsDeleted == false);
            var therapistDto = Mapper.Map<TherapistDto>(therapis);
            return therapistDto;
        }

        public int Update(BaseTherapistDto model, string id)
        {
            var therapist = _db.Therapists.Find(model.Id);
            if (therapist != null)
            {
                try
                {
                    if (_db.Users.Any(th => th.Email == model.Email && th.Id != id))
                    {
                        //email is exist
                        return 2;
                    }
                    therapist.Email = model.Email;
                    therapist.FullLegalName = model.FullLegalName;
                    therapist.LastName = model.LastName;
                    therapist.BirthDate = model.BirthDate;
                    therapist.SIN = model.SIN;
                    therapist.CRCExpiry = model.CRCExpiry;
                    therapist.StaffName = model.StaffName;
                    therapist.JopTitle = model.JopTitle;
                    therapist.IsEmployee = model.IsEmployee;
                    therapist.StartDate = model.StartDate;
                    therapist.TerminationDate = model.TerminationDate;
                    therapist.InstitueNo = model.InstitueNo;
                    therapist.TransitNo = model.TransitNo;
                    therapist.Phone = model.Phone;
                    therapist.PostalCode = model.PostalCode;
                    therapist.EmergancyContact = model.EmergancyContact;
                    therapist.EmergancyPhone = model.EmergancyPhone;
                    therapist.Address = model.Address;
                    therapist.AccountNo = model.AccountNo;
                    therapist.UpdatedBy = "0000";
                    therapist.UpdatedDate = DateTime.Now;
                    _db.SaveChanges();

                    return 1;
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, e);

                    return -1;
                }
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CreateDto</param>
        /// <returns>-1 error or exception</returns>
        /// <returns>1 success</returns>
        public async Task<int> Create(CreateTherapistDto model)
        {
            try
            {
                if (_db.Users.Count(u => u.Email == model.Email) > 0)
                {
                    //email is exist
                    return 2;
                }
                string password = Membership.GeneratePassword(8, 2);
                var therapist = Mapper.Map<Therapist>(model);
                therapist.CreatedBy = "0000";
                var result = await CommonService.CreateUser(therapist, password);
                model.Id = therapist.Id;
                if (result.Succeeded)
                {
                    Rate rate = new Rate
                    {
                        Date = DateTime.Now,
                        TherapistId = therapist.Id,
                        BCValue = model.BcValue,
                        OtherValue = model.OtherValue,
                        CreatedBy = "0000"

                    };
                    _db.Rates.Add(rate);
                    _db.SaveChanges();
                    await CommonService.AddToRole("Therapist", therapist.Id);
                    var body = "<p>Hi {0} <p>Welcome to our web site </p> <p>Username: {0}</p><p>Password : {1}</p><p>Thanks for using our services</p>";
                    await CommonService.SendMail(therapist.Email, "Welcome", string.Format(body, therapist.UserName, password));
                    return 1;
                }
            }
            catch (Exception e)
            {
                //log file
                Logger.Log(LogLevel.Error, e);

                return -1;
            }
            return 0;
        }

        public int RerieveTherapist(string id)
        {
            var therapist = _db.Therapists.FirstOrDefault(a => a.Id == id);
            if (therapist != null)
            {
                try
                {
                    therapist.IsDeleted = false;
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



        public List<TherapistDto> RetrieveList()
        {
            var therapist = _db.Therapists.Where(a => a.IsDeleted == true).ToList();
            var therapistDto = Mapper.Map<List<TherapistDto>>(therapist);
            return therapistDto;
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
