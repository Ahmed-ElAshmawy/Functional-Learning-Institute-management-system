using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Session : BaseModel
    {
        public Session()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey("Therapist")]
        public string TherapistId { get; set; }

        [Required]
        [ForeignKey("ServiceType")]
        public string ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        public DateTime Date { get; set; }
        public virtual Therapist Therapist { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Child> Children { get; set; }

        public virtual ICollection<Slot> Slots { get; set; }

        public SessionStatus Status { get; set; } = SessionStatus.Pending;

        [NotMapped]
        public SessionType Type => Children.Count > 1 ? SessionType.Group : SessionType.Individual;

    }
}