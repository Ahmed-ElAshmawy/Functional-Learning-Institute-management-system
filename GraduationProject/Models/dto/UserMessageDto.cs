using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class UserMessageDto
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string FromId { get; set; }
        public string MessageBody { get; set; }
        public DateTime Date { get; set; }
        public MessageStatus Status { get; set; }
        public string ToId { get; set; }
        public string Data { get; set; }
    }
}