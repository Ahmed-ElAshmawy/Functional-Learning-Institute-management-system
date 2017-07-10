using GraduationProject.Models.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class CreateTherapistDto : BaseTherapistDto
    {
        public string UserName { get; set; }
        public double OtherValue { get; set; }
        public double BcValue { get; set; }
    }
}