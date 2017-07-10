using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ParentEmail: BaseModel
    {
        public ParentEmail()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        [ForeignKey("Parent")]
        [Required]
        public string ParentId { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual Parent Parent { get; set; }
    }
}