using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class CreateTherapistAvailabilityDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsBusy { get; set; } = false;
        public string TherapistId { get; set; }
        public string[] Slots { get; set; }
    }
}