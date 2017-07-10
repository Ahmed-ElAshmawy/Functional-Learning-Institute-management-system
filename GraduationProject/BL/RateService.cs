using AutoMapper;
using GraduationProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace GraduationProject
{
    public class RateService
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public IEnumerable GetAll(string therapistid)
        {
            var rateList = _db.Rates.Where(a => a.TherapistId == therapistid && a.IsDeleted==false).ToList();
            return Mapper.Map<List<RateDto>>(rateList); ;
        }
        public int AddRate(RateDto model)
        {
            var Rate = Mapper.Map<Rate>(model);
            try
            {
                Rate.CreatedBy = "0000";
                _db.Rates.Add(Rate);
                _db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                Logger.Log(LogLevel.Error, e);

                return 0;
            }
        }
        public int DeleteRate(string id,string therapistId)
        {
            try
            {
                _db.Rates.FirstOrDefault(a => a.TherapistId == therapistId && a.Id == id).IsDeleted=true;
                _db.SaveChanges();
                return 1;
            }
            catch(Exception e)
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