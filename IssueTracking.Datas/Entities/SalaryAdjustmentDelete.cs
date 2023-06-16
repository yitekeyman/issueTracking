using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class SalaryAdjustmentDelete
    {
        public Guid Id { get; set; }
        public Guid NewSaId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmpIdNo { get; set; }
        public double Salary { get; set; }
        public string Benefits { get; set; }
        public long Date { get; set; }
        public long EffectiveDate { get; set; }
        public string Reason { get; set; }
        public string JobGrade { get; set; }
        public int StaffProfession { get; set; }
    }
}
