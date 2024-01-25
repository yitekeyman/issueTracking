using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueMilestones
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid MilestoneId { get; set; }
    }
}
