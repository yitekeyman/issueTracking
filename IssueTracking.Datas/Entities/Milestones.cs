using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Milestones
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public long? DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public long? CreatedDate { get; set; }

        public virtual Employee CreatedByNavigation { get; set; }
        public virtual IssuesList Issue { get; set; }
    }
}
