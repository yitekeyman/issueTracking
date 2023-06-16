using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class AttendanceRule
    {
        public AttendanceRule()
        {
            EmpAttendanceRule = new HashSet<EmpAttendanceRule>();
            ShiftAttendanceRule = new HashSet<ShiftAttendanceRule>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EmpAttendanceRule> EmpAttendanceRule { get; set; }
        public virtual ICollection<ShiftAttendanceRule> ShiftAttendanceRule { get; set; }
    }
}
