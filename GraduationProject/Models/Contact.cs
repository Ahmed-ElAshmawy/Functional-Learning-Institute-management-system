using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Contact : BaseModel
    {
        public Contact()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        [Required]
        public string Number { get; set; }
        public string ContactName { get; set; }
        public ContactType ContactType { get; set; }
        public virtual Parent Parent { get; set; }
    }
}