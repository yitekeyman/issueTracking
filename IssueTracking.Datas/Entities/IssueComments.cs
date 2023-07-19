using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueComments
    {
        public Guid Id { get; set; }
        public Guid? IssueId { get; set; }
        public string IssueComment { get; set; }
        public DateTime CommentDate { get; set; }
        public DateTime ModifiedDate { get; set; } // i added this line for editing comments
        public Guid? CommentedBy { get; set; }
        public string CommentResource { get; set; }
        public long? IssueStatus { get; set; }
        public int? Status { get; set; }

        public virtual Employee CommentedByNavigation { get; set; }
        public virtual IssuesList Issue { get; set; }
        public virtual IssueStatusType IssueStatusNavigation { get; set; }
    }
}
