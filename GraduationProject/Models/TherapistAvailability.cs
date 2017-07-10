using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class TherapistAvailability
    {
        public TherapistAvailability()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DefaultValue("false")]
        public bool IsBusy { get; set; } = false;

        [Required]
        [ForeignKey("Slot")]
        public string SlotId { get; set; }

        [Required]
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }

        public virtual Slot Slot { get; set; }

        public virtual Therapist Therapist { get; set; }

    }
}