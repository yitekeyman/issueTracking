using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeBenefit
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool? Status { get; set; }
        public Guid? BenefitTypeId { get; set; }
        public string BenefitName { get; set; }
        public decimal? BenefitValue { get; set; }
        public bool? IsTaxable { get; set; }
        public int? Flag { get; set; }
    }
}
