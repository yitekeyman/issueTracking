using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpAnnualLeaves
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmpIdNo { get; set; }
        public double NoDays { get; set; }
        public long Date { get; set; }
        public string Status { get; set; }
        public double? Used { get; set; }
        public double? Expired { get; set; }
        public string BudgetYear { get; set; }
    }
}
