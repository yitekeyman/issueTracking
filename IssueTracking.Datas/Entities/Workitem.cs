using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Workitem
    {
        public Workitem()
        {
            WorkitemNote = new HashSet<WorkitemNote>();
        }

        public Guid Id { get; set; }
        public long? AssignedRole { get; set; }
        public string AssignedUser { get; set; }
        public string Data { get; set; }
        public long? FromState { get; set; }
        public int? SeqNo { get; set; }
        public long? Trigger { get; set; }
        public Guid? WorkflowId { get; set; }
        public string Description { get; set; }
        public int? Type { get; set; }
        public string DataType { get; set; }

        public virtual Workflow Workflow { get; set; }
        public virtual ICollection<WorkitemNote> WorkitemNote { get; set; }
    }
}
