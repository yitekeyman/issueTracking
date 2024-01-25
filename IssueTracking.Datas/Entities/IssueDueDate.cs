using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueDueDate
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public long? StartTime { get; set; }
        public long? EndTime { get; set; }
        public long? DueDate { get; set; }
        public string Status { get; set; }
    }
}
