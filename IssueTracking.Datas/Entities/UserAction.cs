using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class UserAction
    {
        public long Id { get; set; }
        public long TimeStamp { get; set; }
        public string Username { get; set; }
        public long? ActionTypeId { get; set; }
        public string Remark { get; set; }

        public virtual ActionType ActionType { get; set; }
        public virtual Account UsernameNavigation { get; set; }
    }
}
