using GraduationProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NLog;

namespace GraduationProject.BL
{
    public class InvoiceService
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///    Get all Children in AFU fundtype 
        /// </summary>
        /// <param name="FundType"></param>
        /// <param name="specificDate"></param>
        /// <returns></returns>
        public IEnumerable GetAllChildren(DateTime specificDate,string fundType)
        {
            List<ChildInfoDto> ch_list = new List<ChildInfoDto>();
            ChildInfoDto ch;
            var SessionInMonth= db.Sessions.Where(se => se.Date.Month == specificDate.Month && se.Date.Year == specificDate.Year && se.IsDeleted == false && se.Status == SessionStatus.Finished).ToList();
            var ChildList = SessionInMonth.SelectMany(a => a.Children).Distinct().ToList();
            var ChildListInFundType = ChildList.Where(a => a.FundGroup.ToString() == fundType);
            foreach (var item in ChildListInFundType)
            {
                ch = new ChildInfoDto();
                ch.ChildId = item.Id;
                ch.ChildName = item.FirstName;
                ch.ChildLastName = item.LastName;
                ch.ParentName = item.Parent.FatherName;
                ch_list.Add(ch);
            }
            return ch_list;
        }

        public IEnumerable ChildInvoice(string childId, DateTime specificDate)
        {
            List<InvoiceChildDto> ch_Info = new List<InvoiceChildDto>();
            InvoiceChildDto ch;
            var child_session = db.Sessions.Where(a => a.Children.Any(c => c.Id == childId) && a.Status == SessionStatus.Finished
              && a.Date.Month == specificDate.Month && a.Date.Year == specificDate.Year && a.IsDeleted == false).OrderBy(a=>a.Date).ToList();
            foreach (var item in child_session)
            {
                ch = new InvoiceChildDto();                
               var d = ch_Info.LastOrDefault();
                if(d != null)
                {
                    if (item.Date == d.Date && item.ServiceType.ServiceTypeName == d.TypeService && ch_Info.Count() > 0)
                    {
                        d.Num += ((item.Slots.Count()) * 15);
                        d.NumberOfHour = (TimeSpan.FromMinutes(d.Num).ToString(@"hh\:mm\:ss"));
                        d.TotalAmount = ((d.Num / 60) * d.RatePerHour);
                    }
                    else
                    {
                        ch.BillingNumber = db.ClientFund.FirstOrDefault(a => a.ChildId == childId).BillingNumber;
                        ch.InvoiceDate = specificDate.ToString("MMMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        ch.InvoiceNumber = db.Children.FirstOrDefault(a => a.Id == childId).Initials + "-" + ch.InvoiceDate;
                        ch.RatePerHour = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId && a.Date.Month == specificDate.Month && a.Date.Year == specificDate.Year).ServiceRate;
                        ch.TypeService = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId).ServiceTypeName;
                        ch.Date = item.Date;
                        ch.Num = ((item.Slots.Count()) * 15);
                        TimeSpan span = TimeSpan.FromMinutes(ch.Num);
                        ch.NumberOfHour = span.ToString(@"hh\:mm\:ss");
                        ch.TotalAmount = (ch.Num / 60) * (ch.RatePerHour);
                        ch_Info.Add(ch);
                    }
                }                  
                else
                {
                    ch.BillingNumber = db.ClientFund.FirstOrDefault(a => a.ChildId == childId).BillingNumber;
                    ch.InvoiceDate = specificDate.ToString("MMMM,yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                    ch.InvoiceNumber = db.Children.FirstOrDefault(a => a.Id == childId).Initials + "-" + ch.InvoiceDate;
                    ch.RatePerHour = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId && a.Date.Month == specificDate.Month && a.Date.Year == specificDate.Year).ServiceRate;
                    ch.TypeService = db.ServiceType.FirstOrDefault(a => a.Id == item.ServiceTypeId).ServiceTypeName;
                    ch.Date = item.Date;
                    ch.Num = ((item.Slots.Count()) * 15);
                    TimeSpan span = TimeSpan.FromMinutes(ch.Num);
                    ch.NumberOfHour = span.ToString(@"hh\:mm\:ss");
                    ch.TotalAmount = (ch.Num / 60) * (ch.RatePerHour);
                    ch.ChildName= db.Children.FirstOrDefault(a => a.Id == childId).FirstName;
                    ch.LastName= db.Children.FirstOrDefault(a => a.Id == childId).LastName;
                    ch_Info.Add(ch);
                }    
            }
            //var d = child_session.GroupBy(a => a.Date).Select(x => x.Select(c => c.Slots)).ToList();
            return ch_Info;
        }


      
        public async Task<int> SendInvoice(GList imageFile, string childId,string email)
        {
            try
            {
                // add invoice number
                string f = imageFile.Value.Split(',')[1];
                byte[] bytes = Convert.FromBase64String(f);
                var body = "<p>Dear {0}</p> <p>We are contacting you in regard to a new invoice that has been created on your account. You may find the invoice attached. </p>"
                    + "<p><b>Service Provider Name:</b>Functional Learning Center Inc.<br>"
                    + "<b>Mailing Address:</b> Victoria<br>"
                    + "<b>Victoria City:</b>  D - 1820 Oak Bay Avenue Postal Code: V8R 1B9 <br>"
                    + "<b>Phone Number:</b> (250) 999 - 2028</p><br>"
                    + "<p>Thanks for using our services</p>";
                CommonService.emailName = db.Children.FirstOrDefault(a => a.Id == childId).FirstName + DateTime.Now.Month;
                int sended = await CommonService.SendMail(email, "Invoice" + DateTime.Now.Month, string.Format(body,"Autism Funding Unit"), bytes);
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