using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class SimpleTask
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDetail { get; set; }
        public long DateFrom { get; set; }
        public long DateTo { get; set; }
        public string Calendar { get; set; }
        public Guid SupervisorId { get; set; }
        public long Date { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee Supervisor { get; set; }
    }
}
