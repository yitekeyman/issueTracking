using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Promotion
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmpIdNo { get; set; }
        public string Title { get; set; }
        public long Positions { get; set; }
        public double? Salary { get; set; }
        public string Benefits { get; set; }
        public long Date { get; set; }
        public bool? HasSalaryAdjustment { get; set; }
        public string HasTransfer { get; set; }
        public string JobGrade { get; set; }
        public int? StaffProfession { get; set; }
    }
}
