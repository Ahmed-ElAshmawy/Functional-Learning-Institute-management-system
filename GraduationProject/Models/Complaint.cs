using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Complaint
    {
        public Complaint()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Title { get; set; }
        public string Answer { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public ComplaintStatus Status { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}