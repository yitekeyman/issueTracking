using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class ShiftAttendanceRule
    {
        public ShiftAttendanceRule()
        {
            ShiftDayOfWeek = new HashSet<ShiftDayOfWeek>();
        }

        public long Id { get; set; }
        public long? AttendanceRuleId { get; set; }
        public string Shift { get; set; }
        public string Dayoff { get; set; }
        public int EarlyMinsAllowed { get; set; }
        public int LateMinsAllowed { get; set; }
        public TimeSpan InHour { get; set; }
        public TimeSpan OutHours { get; set; }

        public virtual AttendanceRule AttendanceRule { get; set; }
        public virtual ICollection<ShiftDayOfWeek> ShiftDayOfWeek { get; set; }
    }
}
