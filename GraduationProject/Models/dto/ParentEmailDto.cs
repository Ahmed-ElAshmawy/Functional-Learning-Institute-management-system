using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ParentEmailDto
    {
        public ParentEmailDto()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId">ParentId</param>
        /// <param name="email">Email</param>
        public ParentEmailDto(string parentId, string email)
        {
            ParentId = parentId;
            Email = email;
        }
        public string Id { get; set; }
        [ForeignKey("Parent")]
        [Required]
        public string ParentId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}