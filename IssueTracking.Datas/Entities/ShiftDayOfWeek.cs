using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class ShiftDayOfWeek
    {
        public Guid Id { get; set; }
        public long ShiftRuleId { get; set; }
        public string Day { get; set; }

        public virtual ShiftAttendanceRule ShiftRule { get; set; }
    }
}
