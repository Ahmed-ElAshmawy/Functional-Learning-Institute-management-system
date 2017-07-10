using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class ContactDto
    {
        public ContactDto()
        {
            
        }
        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// <param name="number">number</param>
        /// <param name="contactName">contactName</param>
        /// <param name="contactType">contactType</param>
        public ContactDto(string parentId,string number,string contactName, ContactType contactType)
        {
            ParentId = parentId;
            Number = number;
            ContactName = contactName;
            ContactType = contactType;
        }

        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Number { get; set; }
        public string ContactName { get; set; }
        public ContactType ContactType { get; set; }
    }
}