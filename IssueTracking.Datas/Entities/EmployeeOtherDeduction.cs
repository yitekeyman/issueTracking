using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeOtherDeduction
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public long DeductFor { get; set; }
        public bool Status { get; set; }
    }
}
