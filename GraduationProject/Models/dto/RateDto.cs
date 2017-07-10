using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class RateDto
    {
        public RateDto()
        {
        }
        public RateDto(DateTime date, double bCValue,double otherValue, string therapistId)
        {
            Date = date;
            BCValue = bCValue;
            OtherValue = otherValue;
            TherapistId = therapistId;
        }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double BCValue { get; set; }
        public double OtherValue { get; set; }
        public string TherapistId { get; set; }
    }
}