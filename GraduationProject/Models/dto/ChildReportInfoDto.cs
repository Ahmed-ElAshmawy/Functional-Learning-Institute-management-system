using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ChildReportInfoDto
    {
        public string ChildId { get; set; }
        public string ChildName { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public bool AFU { get; set; }
    }
}