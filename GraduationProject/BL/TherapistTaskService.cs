using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using AutoMapper;
using System.Data.Entity;
using GraduationProject.BL;
using NLog;

namespace GraduationProject
{
    public class TherapistTaskService : IService<TherapistTaskDto>
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        public int Delete(string id)
        {
            return 0;
        }

        public IEnumerable GetAll()
        {
            var Tasks = Mapper.Map<List<TherapistTaskDto>>(_db.Tasks.ToList());
            return Tasks;
        }

        public IEnumerable GetById(string id)
        {
            var dbTask = _db.Tasks.Where(t => t.TherapistId == id && t.IsDeleted == false).ToList();
            return Mapper.Map<List<TherapistTaskDto>>(dbTask);

        }

        public int Update(TherapistTaskDto model, string id)
        {
            var Task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            if (Task != null)
            {
                try
                {
                    Task.DateTime = model.DateTime;
                    Task.Description = model.Description;
                    Task.Duration = model.Duration;
                    Task.TherapistId = model.TherapistId;
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

        public int Create(TherapistTaskDto model)
        {
            model.Id = Guid.NewGuid().ToString();
            TherapistTask Task = Mapper.Map<TherapistTask>(model);
            try
            {
                Task.CreatedBy = "0000";
                _db.Tasks.Add(Task);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return 0;
            }
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        TherapistTaskDto IService<TherapistTaskDto>.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}