using GraduationProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduationProject.Models.dto;
using System.Threading.Tasks;
using NLog;

namespace GraduationProject.BL
{
    public class ParentReport
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        SessionService _S = new SessionService();
        public IEnumerable ReportParent(string childId, DateTime specificDate)
        {
            List<ParentReportDto> pr = new List<ParentReportDto>();
            ParentReportDto p;
            var child_session = db.Sessions.Where(a => a.Children.Any(c => c.Id == childId) && a.Status == SessionStatus.Finished
              && a.Date.Month == specificDate.Month && a.Date.Year == specificDate.Year && a.IsDeleted == false);
            foreach (var x in child_session)
            {
                var num_slot = x.Slots.Count();
                double hr = (double)(num_slot * 15) / 60;
                var rateValue = db.ServiceType.FirstOrDefault(a => a.Id == x.ServiceTypeId && a.Date.Month == specificDate.Month && a.Date.Year == specificDate.Year).ServiceRate;
                TimeSpan span = TimeSpan.FromMinutes(num_slot * 15);
                p = new ParentReportDto();
                p.Name = db.ServiceType.FirstOrDefault(a=>a.Id==x.ServiceTypeId).ServiceTypeName;
                p.Hours = span.ToString(@"hh\:mm\:ss");
                p.Date = x.Date.ToString("MMMM dddd, yyyy");
                p.RatePerHr = rateValue;
                p.Amount = p.RatePerHr * hr;
                p.Start= x.Slots.Select(a => a.From).First().ToShortTimeString();
                p.Initials = db.Children.FirstOrDefault(a => a.Id == childId).Initials;
                p.ChildName = db.Children.FirstOrDefault(a => a.Id == childId).FirstName;
                p.ParentName = db.Children.FirstOrDefault(a => a.Id == childId).LastName;
                pr.Add(p);
            }
            return pr;
        }


        public async Task<int> SendInvoiceToParent(GList imageFile, string childId)
        {
            try
            {
                // add invoice number
                string f = imageFile.Value.Split(',')[1];
                byte[] bytes = Convert.FromBase64String(f);
                var parent = db.Parents.FirstOrDefault(a => a.Children.Any(c => c.Id == childId));
                var body = "<p>Dear {0}</p> <p>We are contacting you in regard to a new invoice that has been created on your account. You may find the invoice attached. </p>"
                    + "<p><b>Service Provider Name:</b>Functional Learning Center Inc.<br>"
                    + "<b>Mailing Address:</b> Victoria<br>"
                    + "<b>Victoria City:</b>  D - 1820 Oak Bay Avenue Postal Code: V8R 1B9 <br>"
                    + "<b>Phone Number:</b> (250) 999 - 2028</p><br>"
                    + "<p>Thanks for using our services</p>";
                CommonService.emailName = db.Children.FirstOrDefault(a => a.Id == childId).FirstName + DateTime.Now.Month;
                int sended = await CommonService.SendMail(parent.Email, "Invoice" + DateTime.Now.Month, string.Format(body, parent.UserName), bytes);
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