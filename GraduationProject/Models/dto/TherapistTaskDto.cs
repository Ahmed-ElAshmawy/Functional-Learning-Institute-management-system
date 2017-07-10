using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GraduationProject.Models
{
    public class TherapistTaskDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string TherapistId { get; set; }
        public double Duration { get; set; }
    }
}