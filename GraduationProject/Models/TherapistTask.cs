using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models    
{
    public class TherapistTask: BaseModel
    {
        public TherapistTask()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }

        public virtual Therapist Therapist { get; set; }

        public double Duration { get; set; }
    }
}