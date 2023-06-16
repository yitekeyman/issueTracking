using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeProfile
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmpIdNo { get; set; }
        public string EmployeeName { get; set; }
        public string Bank { get; set; }
        public string BankBranch { get; set; }
        public string BankAccount { get; set; }
        public string Tin { get; set; }
        public double? GrossSalary { get; set; }
        public decimal? CostSharingPayableAmount { get; set; }
        public decimal? TotalCostSharingDebt { get; set; }
        public int? CostSharingStatus { get; set; }
        public string PfNumber { get; set; }
    }
}
