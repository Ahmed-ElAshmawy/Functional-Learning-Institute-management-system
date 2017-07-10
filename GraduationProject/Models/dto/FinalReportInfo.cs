using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class FinalReportInfo
    {
        public string TherapistName { get; set; }
        public double HourlyRate { get; set; }
        public string HoursWorked { get; set; }
        public string OvertimeHours { get; set; }
        public double Amount { get; set; }
        public string Desciption { get; set; }
        public string Date { get; set; }
        public string ServiceType { get; set; }
        public string SessionType { get; set; }
        public double Num { get; set; }
    }
}