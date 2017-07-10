using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Rate: BaseModel
    {
        public Rate()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string  Id { get; set; }
        public DateTime Date { get; set; }

        public double BCValue { get; set; }
        public double OtherValue { get; set; }

        [Required]
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }

        public virtual Therapist Therapist { get; set; }
    }
}