using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace GraduationProject.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedDate=DateTime.Now;
           // this.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
        }
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string DeletedBy { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; } = false;

    }
}