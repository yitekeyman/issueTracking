using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Workflow
    {
        public Workflow()
        {
            WorkflowDocument = new HashSet<WorkflowDocument>();
            Workitem = new HashSet<Workitem>();
        }

        public Guid Id { get; set; }
        public long? TypeId { get; set; }
        public string Initiator { get; set; }
        public string Description { get; set; }
        public long? TimeStamp { get; set; }
        public int OldState { get; set; }
        public Guid? CurrentWorkItem { get; set; }
        public int CurrentState { get; set; }
        public string Employee { get; set; }

        public virtual WorkflowType Type { get; set; }
        public virtual ICollection<WorkflowDocument> WorkflowDocument { get; set; }
        public virtual ICollection<Workitem> Workitem { get; set; }
    }
}
