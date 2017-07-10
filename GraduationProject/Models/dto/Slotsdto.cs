using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models.dto
{
    public class Slotsdto
    {
        public Slotsdto()
        {
            this.Slots= new List<string>();
        }
        public string Id { get; set; }
        public List<string> Slots { get; set; }
    }
}