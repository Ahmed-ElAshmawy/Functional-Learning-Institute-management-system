using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GraduationProject.Models.dto;

namespace GraduationProject.Models
{
    public class ParentDto: BaseParentDto
    {

        public ParentDto()
        {
            this.Children= new List<GList>();
            this.Emails= new List<ParentEmailDto>();
            this.Contacts=new List<ContactDto>();
        }

        
        public string Address => $"{StreetName} {StreetNo} {Province} {City}";
       
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public  List<GList> Children { get; set; }
        public  List<ContactDto> Contacts { get; set; }
        public  List<ParentEmailDto> Emails { get; set; }
    }   
}