using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models.dto
{
    public class ComplaintDto
    {
        public ComplaintDto()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">ParentId</param>
        /// <param name="body">body</param>
        /// <param name="title">title</param>

        public ComplaintDto(string userId, string body,string title)
        {
            UserId = userId;
            Body = body;
            Title = title;
        }
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public string UserName { get; set; }

        public ComplaintStatus Status { get; set; }
    }
}
