using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public enum FundGroup
    {
        Under6,
        From6To18,
        Over18
    }
    public enum FundType
    {
        FundGroupUnder6,
        FundGroupFrom6To18,
        FundGroupOver18,
        HomeFunding,
        OutOfPocket,
        AFU
    }
    public enum ContactType
    {
        Emergancy,
        Home,
        CellPhone,
        Work
    };

    public enum MessageStatus
    {
        Read,
        UnRead
    }

    public enum SessionStatus
    {
        Pending,
        Finished,
        Canceled,
        WaitingAdminConfirmation
    }

    public enum SessionType
    {
        Individual,
        Group
    }
    public enum ComplaintStatus
    {
        Pending,
        Resolved
    }

    public class ChildSessions
    {
        public ChildSessions()
        {

        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        public ChildSessions(string id, string name, int sessioncount)
        {
            Name = name;
            Id = id;
            SessionCount = sessioncount;
        }
        public string Name { get; set; }
        public int SessionCount { get; set; }
        public string Id { get; set; }
    }

    public class GList
    {
        public GList()
        {
            
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="value">Value</param>
        public GList(string id,string value)
        {
            Value = value;
            Id = id;
        }
        public string Value { get; set; }
        public string Id { get; set; }
    }
}