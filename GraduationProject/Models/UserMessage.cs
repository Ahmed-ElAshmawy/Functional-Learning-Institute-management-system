using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class UserMessage
    {
        public UserMessage()
        {
            this.Id = Guid.NewGuid().ToString();
            Status = MessageStatus.UnRead;
        }
        [Key]
        public string Id { get; set; }

        [ForeignKey("From")]
        public string FromId { get; set; }
        public string MessageBody { get; set; }
        public DateTime Date { get; set; }
        public MessageStatus Status { get; set; }
        public string ToId { get; set; }
        public string Data { get; set; }
        public virtual ApplicationUser From { get; set; }
    }
}