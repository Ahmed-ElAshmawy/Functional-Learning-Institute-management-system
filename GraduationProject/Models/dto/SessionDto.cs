using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class SessionDto
    {
        public SessionDto()
        {
            this.Children = new List<GList>();
        }
        public string Id { get; set; }
        public string TherapistName { get; set; }
        public string TherapistId { get; set; }
        public DateTime Date { get; set; }
        public List<GList> Children { get; set; }
        public List<SlotDto> Slots { get; set; }
        public string Location { get; set; }
        public string ServiceTypeId { get; set; }

        public SessionStatus Status { get; set; } = SessionStatus.Pending;
        public SessionType Type => Children.Count > 1 ? SessionType.Group : SessionType.Individual;
    }
}