using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class BaseParentDto
    {
        [Required]
        public string Email { get; set; }

        public string Id { get; set; }
        [Required]

        public string MotherName { get; set; }
        [Required]

        public string FatherName { get; set; }
        [Required]

        public string PostalCode { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string StreetName { get; set; }
        [Required]

        public int StreetNo { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        public string Province { get; set; }

    }
}