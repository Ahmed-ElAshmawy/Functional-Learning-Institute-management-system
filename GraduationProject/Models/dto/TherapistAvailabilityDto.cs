using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class TherapistAvailabilityDto
    {
        public string Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DefaultValue("false")]
        public bool IsBusy { get; set; } = false;
        [Required]
        public string TherapistId { get; set; }
        public string SlotId { get; set; }
        public SlotDto Slot { get; set; }
    }
}