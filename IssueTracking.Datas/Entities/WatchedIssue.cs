using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class WatchedIssue
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid EmployeeId { get; set; }
        public long? WatDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual IssuesList Issue { get; set; }
    }
}
