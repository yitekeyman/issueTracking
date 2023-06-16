using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpLeaveTermination
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid LeaveId { get; set; }
        public string AllowedBy { get; set; }
        public long Date { get; set; }
        public string Reason { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public Guid[] AffectedAnnualLeaves { get; set; }
    }
}
