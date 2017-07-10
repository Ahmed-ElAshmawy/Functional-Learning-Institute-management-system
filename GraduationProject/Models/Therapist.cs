using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace GraduationProject.Models
{
    [Table("Therapist")]
    public class Therapist : ApplicationUser
    {
       
        [Required]
        public string StaffName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullLegalName { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string SIN { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone { get; set; }

        public DateTime CRCExpiry { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        [Required]
        public string TransitNo { get; set; }
        [Required]
        public bool IsEmployee { get; set; }
        [Required]
        public string InstitueNo { get; set; }
        [Required]
        public string AccountNo { get; set; }
        public string EmergancyContact { get; set; }
        public string EmergancyPhone { get; set; }
        [Required]
        public string JopTitle { get; set; }
        public string PostalCode { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<TherapistTask> Tasks { get; set; }

        public virtual ICollection<TherapistAvailability> TherapistAvailabilities { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}