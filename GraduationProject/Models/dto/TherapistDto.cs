using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GraduationProject.Models.dto;

namespace GraduationProject.Models
{
    public class TherapistDto:BaseTherapistDto
    {
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RateDto> Rates { get; set; }
        public List<TherapistTaskDto> Tasks { get; set; }
        public List<TherapistAvailabilityDto> TherapistAvailabilities { get; set; }
        public List<SessionDto> Sessions { get; set; }
    }
}