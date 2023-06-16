using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeLoanData
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public long? LoanType { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public Guid Guarantor { get; set; }
        public long? Date { get; set; }
        public int? Status { get; set; }
        public string Name { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public decimal InterestFreeAmount { get; set; }
        public long LoanPeriodInMonth { get; set; }
        public decimal CurrentPayroll { get; set; }
        public decimal InterestedAmount { get; set; }
    }
}
