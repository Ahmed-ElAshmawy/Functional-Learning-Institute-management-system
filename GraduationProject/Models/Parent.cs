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
    [Table("Parent")]
    public class Parent : ApplicationUser
    {
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

        [NotMapped]
        public string Address => $"{StreetName} {StreetNo} {Province} {City}";

        [Required]
        [DefaultValue("BC")]
        public string Province { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Child> Children { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<ParentEmail> Emails { get; set; }

    }
}