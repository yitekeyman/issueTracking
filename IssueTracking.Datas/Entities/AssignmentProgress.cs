using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class AssignmentProgress
    {
        public Guid Id { get; set; }
        public Guid? AssignmentId { get; set; }
        public Guid? AssignEmpId { get; set; }
        public decimal? ProgressPercent { get; set; }
        public int? Status { get; set; }
        public long? Date { get; set; }
        public Guid ActivityId { get; set; }
        public int Type { get; set; }

        public virtual Assignments Assignment { get; set; }
    }
}
