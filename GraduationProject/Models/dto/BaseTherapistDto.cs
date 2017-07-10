using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class BaseTherapistDto
    {
        public string Id { get; set; }
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
        public bool IsEmployee { get; set; } = true;
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
        public string Email { get; set; }

    }
}