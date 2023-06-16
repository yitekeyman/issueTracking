using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpAssignments
    {
        public Guid Id { get; set; }
        public Guid? AssignmentId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Location { get; set; }
        public Guid? SubstituteId { get; set; }
        public long? StartDate { get; set; }
        public long? EndDate { get; set; }

        public virtual Assignments Assignment { get; set; }
    }
}
