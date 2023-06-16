using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueAssigned
    {
        public Guid Id { get; set; }
        public Guid? IssueId { get; set; }
        public Guid? AssignedBy { get; set; }
        public Guid? AssignedTo { get; set; }
        public long? AssignDate { get; set; }

        public virtual Employee AssignedByNavigation { get; set; }
        public virtual Employee AssignedToNavigation { get; set; }
        public virtual IssuesList Issue { get; set; }
    }
}
