using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpAttendanceRule
    {
        public Guid EmployeeId { get; set; }
        public long RuleId { get; set; }
        public string DefaultRule { get; set; }
        public long? StartDate { get; set; }
        public long? EndDate { get; set; }
        public string EmpIdNo { get; set; }
        public Guid Id { get; set; }

        public virtual AttendanceRule Rule { get; set; }
    }
}
