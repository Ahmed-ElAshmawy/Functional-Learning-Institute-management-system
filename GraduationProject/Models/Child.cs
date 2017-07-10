using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Child:BaseModel
    {
        public Child()
        {
            this.CreatedDate = DateTime.Now;
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string MedicalProviderPhone { get; set; }

        public string MedicalProvider { get; set; }
        [Required]
        [DefaultValue("AFU")]
        public FundType FundGroup { get; set; } = FundType.AFU;

        public DateTime ClientSince { get; set; }

        public string SOLocation { get; set; }

        [NotMapped]
        public string AFUName => $"{Initials} {LastName}";
        public string Initials { get; set; }
        [Required]
        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual Parent Parent { get; set; }
        public virtual ICollection<ChildrenAvailability> ChildrenAvailabilities { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<ClientFund> ClientFunds { get; set; }
    }
}