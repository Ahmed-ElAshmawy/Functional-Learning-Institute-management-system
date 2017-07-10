using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class InvoiceChildDto
    {

        public DateTime Date { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public String BillingNumber { get; set; }
        public string TypeService { get; set; }
        public double Num { get; set; }
        public string NumberOfHour { get; set; }
        public double RatePerHour { get; set; }
        public double TotalAmount { get; set; }
        public String ChildName { get; set; }
        public String LastName { get; set; }
    }
}