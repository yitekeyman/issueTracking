using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class WorkflowType
    {
        public WorkflowType()
        {
            Workflow = new HashSet<Workflow>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Workflow> Workflow { get; set; }
    }
}
