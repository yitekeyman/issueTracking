using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class DeletedIssuesList
    {
        public Guid Id { get; set; }
        public Guid OldIssueId { get; set; }
        public string IssueDetails { get; set; }
        public Guid ModifiyedBy { get; set; }
        public long? ModifiyedDate { get; set; }
        public long? Indexs { get; set; }

        public virtual Employee ModifiyedByNavigation { get; set; }
        public virtual IssuesList OldIssue { get; set; }
    }
}
