using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class ActionTracker
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public string ActionType { get; set; }
        public string ActionDetails { get; set; }
        public string Remark { get; set; }
        public long ActionDate { get; set; }
    }
}
