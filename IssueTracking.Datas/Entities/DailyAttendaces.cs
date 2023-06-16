using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class DailyAttendaces
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public long? Date { get; set; }
        public string Day { get; set; }
        public string Shift { get; set; }
        public string EmpIdNo { get; set; }
        public bool? Abscent { get; set; }
        public TimeSpan In1 { get; set; }
        public TimeSpan Out1 { get; set; }
        public TimeSpan? Othrs { get; set; }
        public TimeSpan? WorkHours { get; set; }
        public double? LateMin { get; set; }
        public double? EarlyMin { get; set; }
        public TimeSpan? LeaveHrs { get; set; }
        public string From { get; set; }
    }
}
