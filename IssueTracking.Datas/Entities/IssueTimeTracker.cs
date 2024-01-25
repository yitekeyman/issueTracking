using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueTimeTracker
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid UserId { get; set; }
        public long StartTime { get; set; }
        public long? EndTime { get; set; }
        public string Status { get; set; }
    }
}
