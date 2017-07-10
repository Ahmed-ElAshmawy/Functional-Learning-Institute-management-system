using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class CreateParentDto: BaseParentDto
    {                
        [Required]
        public string CellContactName { get; set; }
        [Required]
        public string CellContactNumber { get; set; }
        [Required]
        public string EmergancyContactName { get; set; }
        [Required]
        public string EmergancyContactNumber { get; set; }
    }
}