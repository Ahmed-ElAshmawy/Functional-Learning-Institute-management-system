using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class BiMonthInfo
    {
        public string TherapistName { get; set; }
        public string ChildName { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string ServiceType { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public double HourlyRate { get; set; }
        public string SessionType { get; set; }
    }
}