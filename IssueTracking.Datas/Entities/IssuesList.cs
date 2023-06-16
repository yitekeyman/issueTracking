using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssuesList
    {
        public IssuesList()
        {
            DeletedIssuesList = new HashSet<DeletedIssuesList>();
            IssueAssigned = new HashSet<IssueAssigned>();
            IssueComments = new HashSet<IssueComments>();
            IssueNotification = new HashSet<IssueNotification>();
            Labels = new HashSet<Labels>();
            Milestones = new HashSet<Milestones>();
            WatchedIssue = new HashSet<WatchedIssue>();
        }

        public Guid Id { get; set; }
        public long? IssueTypeId { get; set; }
        public string OtherIssue { get; set; }
        public string[] PolicyNo { get; set; }
        public Guid? BranchId { get; set; }
        public string IssueDescription { get; set; }
        public Guid? IssueRequestedBy { get; set; }
        public long? IssueRequestedDate { get; set; }
        public Guid? IssueRespondBy { get; set; }
        public long? IssueRespondDate { get; set; }
        public Guid? IssueClosedBy { get; set; }
        public long? IssueClosedDate { get; set; }
        public long? IssuePriority { get; set; }
        public long? IssueStatus { get; set; }
        public long? IssueRaisedSluId { get; set; }
        public string IssueResource { get; set; }
        public string Ticket { get; set; }

        public virtual DepartmentSchema Branch { get; set; }
        public virtual IssuePriorityType IssuePriorityNavigation { get; set; }
        public virtual Employee IssueRequestedByNavigation { get; set; }
        public virtual IssueStatusType IssueStatusNavigation { get; set; }
        public virtual IssueTypeList IssueType { get; set; }
        public virtual ICollection<DeletedIssuesList> DeletedIssuesList { get; set; }
        public virtual ICollection<IssueAssigned> IssueAssigned { get; set; }
        public virtual ICollection<IssueComments> IssueComments { get; set; }
        public virtual ICollection<IssueNotification> IssueNotification { get; set; }
        public virtual ICollection<Labels> Labels { get; set; }
        public virtual ICollection<Milestones> Milestones { get; set; }
        public virtual ICollection<WatchedIssue> WatchedIssue { get; set; }
    }
}
