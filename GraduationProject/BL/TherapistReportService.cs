using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduationProject.Models;
using System.Collections;
using System.Threading.Tasks;
using GraduationProject.BL;
using NLog;

namespace GraduationProject
{
    
    public class TherapistReportService
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get All therapist in certain month
        /// </summary>
        /// <param name="specificDate"></param>
        /// <returns></returns>
        public IEnumerable AllTherapistInMonth(DateTime specificDate)
        {
            List<TherapistReportInfoDto> therapistList = new List<TherapistReportInfoDto>();
            TherapistReportInfoDto therapistInfo;
            var sessioInMonth = db.Sessions.Where(se => se.Date.Month == specificDate.Month && se.Date.Year == specificDate.Year && se.IsDeleted == false && se.Status == SessionStatus.Finished).ToList();
            var therapistInMonth = sessioInMonth.Select(a => a.Therapist).Distinct().ToList();
            foreach (var item in therapistInMonth)
            {
                therapistInfo = new TherapistReportInfoDto();
                therapistInfo.TherapistId = item.Id;
                therapistInfo.TherapistName = item.FullLegalName;
                therapistInfo.StaffName = item.StaffName;
                therapistList.Add(therapistInfo);
            }
            return therapistList;
        }
        public IEnumerable GetBiMonthReporrt(string therapistId,DateTime specificDate,string reportType)
        {
            List<BiMonthInfo> th_info = new List<BiMonthInfo>();
            BiMonthInfo th;
            double num;
            List<Session> sessioInMonth;
            if (reportType=="BI")
            {
                sessioInMonth = db.Sessions.Where(se => se.Date.Day >= 1 && se.Date.Day <= 15 && se.Date.Month == specificDate.Month && se.Date.Year == specificDate.Year && se.IsDeleted == false && se.Status == SessionStatus.Finished).OrderBy(a => a.Date).ToList();
            }
            else
            {
                sessioInMonth = db.Sessions.Where(se => se.Date.Day >= 16 && se.Date.Day <= 31 && se.Date.Month == specificDate.Month && se.Date.Year == specificDate.Year && se.IsDeleted == false && se.Status == SessionStatus.Finished).OrderBy(a => a.Date).ToList();
            }
            
            foreach (var item in sessioInMonth)
            {
                th = new BiMonthInfo();
                th.TherapistName = db.Therapists.FirstOrDefault(a => a.Id == therapistId).FullLegalName;
                th.ChildName = item.Children.Select(p => p.FirstName).First();
                th.Date = item.Date.ToString("MMMM dddd, yyyy");
                th.ServiceType = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId).ServiceTypeName;
                if (th.ServiceType == "BC")
                {
                    th.HourlyRate = db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == specificDate.Month && r.Date.Month == specificDate.Month).BCValue;
                }
                else
                {
                    th.HourlyRate = db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == specificDate.Month && r.Date.Month == specificDate.Month).OtherValue;
                }
                th.StartTime =item.Slots.Select(a => a.From).First().ToShortTimeString();
                num = ((item.Slots.Count()) * 15);
                TimeSpan span = TimeSpan.FromMinutes(num);
                th.Duration = span.ToString(@"hh\:mm\:ss");
                th.Location = "must be  enter";
                th.Amount = (double)(num / 60) * th.HourlyRate;
                th.SessionType = item.Type.ToString();
                th_info.Add(th);
            }
            return th_info;
        }

        public IEnumerable TherapistReport(string therapistId, DateTime spescificDate)
        {
            List<FinalReportInfo> therapistReport = new List<FinalReportInfo>();
            FinalReportInfo th;
            //double num;
            var therapist_session = db.Sessions.Where(se => se.Date.Month == spescificDate.Month && se.Date.Year == spescificDate.Year && se.TherapistId == therapistId &&
                se.IsDeleted == false && se.Status == SessionStatus.Finished).OrderBy(a=>a.Date).ToList();
            foreach (var item in therapist_session)
            {
                th = new FinalReportInfo();
                var d = therapistReport.LastOrDefault();
                if(d != null)
                {
                    if(item.Date.ToString("MMMM dd, yyyy")==d.Date &&  item.ServiceType.ServiceTypeName == d.ServiceType && therapistReport.Count() > 0)
                    {
                        d.Num += (item.Slots.Count() * 15);
                        d.HoursWorked = (TimeSpan.FromMinutes(d.Num).ToString(@"hh\:mm\:ss"));
                        d.Amount= ((d.Num / 60) * d.HourlyRate);
                    }
                    else
                    {
                        th.TherapistName = db.Therapists.FirstOrDefault(a => a.Id == item.TherapistId).FullLegalName;
                        th.ServiceType = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId).ServiceTypeName;
                        if (th.ServiceType == "BC")
                        {
                            th.HourlyRate = db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == spescificDate.Month && r.Date.Month == spescificDate.Month).BCValue;
                        }
                        else
                        {
                            th.HourlyRate = db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == spescificDate.Month && r.Date.Month == spescificDate.Month).OtherValue;
                        }
                        th.Num = (item.Slots.Count() * 15);
                        TimeSpan span1 = TimeSpan.FromMinutes(th.Num);
                        th.HoursWorked = span1.ToString(@"hh\:mm\:ss");
                        th.Amount = (double)(th.Num / 60) * th.HourlyRate;
                        th.Date = item.Date.ToString("MMMM dd, yyyy");
                        th.SessionType = item.Type.ToString();
                        therapistReport.Add(th);
                    }
                }else
                {
                    th.TherapistName = db.Therapists.FirstOrDefault(a => a.Id == item.TherapistId).FullLegalName;
                    th.ServiceType = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId).ServiceTypeName;
                    if (th.ServiceType == "BC")
                    {
                        th.HourlyRate = db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == spescificDate.Month && r.Date.Month == spescificDate.Month).BCValue;
                    }
                    else
                    {
                        th.HourlyRate = db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == spescificDate.Month && r.Date.Month == spescificDate.Month).OtherValue;
                    }
                    th.Num = (item.Slots.Count() * 15);
                    TimeSpan span1 = TimeSpan.FromMinutes(th.Num);
                    th.HoursWorked = span1.ToString(@"hh\:mm\:ss");
                    th.Amount = (double)(th.Num / 60) * th.HourlyRate;
                    th.Date = item.Date.ToString("MMMM dd, yyyy");
                    th.SessionType =item.Type.ToString();
                    therapistReport.Add(th);
                }
            }
            return therapistReport;
        }
        public IEnumerable GetTherapistTask(string therapistId,DateTime spescificDate)
        {
            List<FinalReportInfo> therapistReport = new List<FinalReportInfo>();
            FinalReportInfo th;
            double num;
            var allTheraistTask = db.Tasks.Where(a => a.TherapistId == therapistId && a.DateTime.Month == spescificDate.Month && a.DateTime.Year == spescificDate.Year);
            foreach (var item in allTheraistTask)
            {
                th = new FinalReportInfo();
                th.TherapistName = db.Therapists.FirstOrDefault(a => a.Id == item.TherapistId).FullLegalName;          
                th.Date = item.DateTime.ToString("MMMM dd, yyyy");
                th.HourlyRate= db.Rates.FirstOrDefault(r => r.TherapistId == item.TherapistId && r.Date.Month == spescificDate.Month && r.Date.Month == spescificDate.Month).OtherValue;
                num = item.Duration * 60;
                TimeSpan span = TimeSpan.FromMinutes(num);
                th.OvertimeHours = span.ToString(@"hh\:mm\:ss");     
                th.Amount = (double)(num/60)* th.HourlyRate;
                th.Desciption = item.Description;
               
                therapistReport.Add(th);
            }
            return therapistReport;
        }

        public async Task<int> SendReport(GList imageFile, string therapistId)
        {
            try
            {

                var therapist = db.Therapists.FirstOrDefault(a => a.Id == therapistId);
                string f = imageFile.Value.Split(',')[1];
                byte[] bytes = Convert.FromBase64String(f);
                var body = "<p>Dear {0}</p> <p><b>Type Of Report :</b>{1}</p><p>We are contacting you in regard to a new invoice that has been created on your account. You may find the Report attached. </p>"
                    + "<p><b>Service Provider Name:</b>Functional Learning Center Inc.<br>"
                    + "<b>Mailing Address:</b> Victoria<br>"
                    + "<b>Victoria City:</b>  D - 1820 Oak Bay Avenue Postal Code: V8R 1B9 <br>"
                    + "<b>Phone Number:</b> (250) 999 - 2028</p><br>"
                    + "<p>Thanks for using our services</p>";
                CommonService.emailName = db.Children.FirstOrDefault(a => a.Id == therapistId).FirstName + DateTime.Now.Month;
                int sended = await CommonService.SendMail(therapist.Email, "Invoice" + DateTime.Now.Month, string.Format(body,therapist.FullLegalName), bytes);
                if (sended != -1)
                    return 1;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e);
                return -1;
            }
            return -1;
        }
    }
}