using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueComments
    {
        public Guid Id { get; set; }
        public Guid? IssueId { get; set; }
        public string IssueComment { get; set; }
        public long? CommentDate { get; set; }
        public Guid? CommentedBy { get; set; }
        public string CommentResource { get; set; }
        public long? IssueStatus { get; set; }
        public int? Status { get; set; }

        public virtual Employee CommentedByNavigation { get; set; }
        public virtual IssuesList Issue { get; set; }
        public virtual IssueStatusType IssueStatusNavigation { get; set; }
    }
}
