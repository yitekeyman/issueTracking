using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeLoanAmortization
    {
        public Guid Id { get; set; }
        public Guid LoanDataId { get; set; }
        public Guid EmployeeId { get; set; }
        public long PaymentDate { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal ScheduledPayment { get; set; }
        public decimal? ExtraPayment { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal? CumulativeInterest { get; set; }
        public long? PaidDate { get; set; }
        public Guid PaidBy { get; set; }
        public bool Status { get; set; }
    }
}
