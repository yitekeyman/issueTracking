using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Assignments
    {
        public Assignments()
        {
            AssignmentProgress = new HashSet<AssignmentProgress>();
            EmpAssignments = new HashSet<EmpAssignments>();
        }

        public Guid Id { get; set; }
        public Guid? DepartmentId { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public long DateFrom { get; set; }
        public int? Status { get; set; }
        public int Type { get; set; }
        public long? DateTo { get; set; }

        public virtual ICollection<AssignmentProgress> AssignmentProgress { get; set; }
        public virtual ICollection<EmpAssignments> EmpAssignments { get; set; }
    }
}
