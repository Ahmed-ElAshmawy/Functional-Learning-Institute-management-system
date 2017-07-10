using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;

namespace GraduationProject.Models
{
    public class ChildrenAvailability
    {
        public ChildrenAvailability()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DefaultValue("false")]
        public bool IsBusy { get; set; } = false;

        [Required]
        [ForeignKey("Slot")]
        public string SlotId { get; set; }

        [Required]
        [ForeignKey("Child")]
        public string ChildId { get; set; }

        public virtual Slot Slot { get; set; }

        public virtual Child Child { get; set; }

        public string Location { get; set; }
    }
}