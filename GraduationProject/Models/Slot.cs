using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Slot 
    {
        public Slot()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [DataType(DataType.Time)]
        [Required]
        public DateTime From { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime To { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

    }
}