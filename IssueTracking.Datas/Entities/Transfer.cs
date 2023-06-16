using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Transfer
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmpIdNo { get; set; }
        public Guid TransferFrom { get; set; }
        public Guid TransferTo { get; set; }
        public long TransferDate { get; set; }
        public string Reasons { get; set; }
        public double? Salary { get; set; }
        public string Benefits { get; set; }
        public bool? HasSalaryAdjustment { get; set; }
        public string JobGrade { get; set; }
        public int? StaffProfession { get; set; }
        public string Title { get; set; }
        public long? Position { get; set; }
        public string DeletedSalary { get; set; }
    }
}
