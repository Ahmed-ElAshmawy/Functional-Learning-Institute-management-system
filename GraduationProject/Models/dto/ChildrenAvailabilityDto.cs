using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ChildrenAvailabilityDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsBusy { get; set; } = false;
        public string SlotId { get; set; }        
        public string ChildId { get; set; }
        public  SlotDto Slot { get; set; }
        public string Location { get; set; }
    }
}