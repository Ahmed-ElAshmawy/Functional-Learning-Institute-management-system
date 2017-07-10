using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ServiceType
    {
        public string Id { get; set; }
        public string ServiceTypeName { get; set; }
        public double ServiceRate { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}