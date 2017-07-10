using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ClientFund
    {
        public ClientFund()
        {
            this.Id=Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        [Required]
        public string BillingNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Amount { get; set; }
        public double Used { get; set; }
        public double Remaining { get; set; }
        [ForeignKey("Child")]
        public string ChildId { get; set; }
        public virtual Child Child { get; set; }
    }
}