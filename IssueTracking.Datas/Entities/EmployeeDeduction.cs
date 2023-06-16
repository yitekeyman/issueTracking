using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeDeduction
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? DeductionId { get; set; }
        public bool? Status { get; set; }
    }
}
