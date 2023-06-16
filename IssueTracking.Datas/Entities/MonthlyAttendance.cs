using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class MonthlyAttendance
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Abscent { get; set; }
        public Guid? AttendanceId { get; set; }
        public string Days { get; set; }
        public string EarlyMin { get; set; }
        public string LateMin { get; set; }
        public string Holiday { get; set; }
        public string HolidayOt { get; set; }
        public string OffDays { get; set; }
        public string WeekendOt { get; set; }
        public string WorkDays { get; set; }
        public long? StartDate { get; set; }
        public long? EndDate { get; set; }
        public string EmpIdNo { get; set; }
    }
}
