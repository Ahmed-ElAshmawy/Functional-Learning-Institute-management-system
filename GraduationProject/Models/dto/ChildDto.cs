using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ChildDto
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MedicalProviderPhone { get; set; }
        public string MedicalProvider { get; set; }
        public DateTime ClientSince { get; set; }
        public string SOLocation { get; set; }
        public string AFUName => $"{Initials} {LastName}";
        public string Initials { get; set; }
        public FundType FundGroup { get; set; }
        public string ParentId { get; set; }
        public string BillingNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Amount { get; set; }
        public double Used { get; set; }
        public double Remaining { get; set; }
    }
}