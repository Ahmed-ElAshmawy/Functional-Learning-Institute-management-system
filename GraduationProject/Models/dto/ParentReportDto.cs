using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ParentReportDto
    {
        public string ChildName { get; set; }
        public string ParentName { get; set; }
        public string Initials { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Hours { get; set; }
        public double RatePerHr { get; set; }
        public string Start { get; set; }
        public double Amount { get; set; }
    }
}