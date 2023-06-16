using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class PayrollComponent
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmpIdNo { get; set; }
        public long? PeriodId { get; set; }
        public decimal? NoOfDays { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TransportAllow { get; set; }
        public decimal? TaxableTransAllow { get; set; }
        public decimal? RepereAllow { get; set; }
        public decimal? HardshipAllow { get; set; }
        public decimal? OtherAllow { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? GrossIncome { get; set; }
        public decimal? TaxableIncome { get; set; }
        public decimal? IncomeTax { get; set; }
        public decimal? PfOrPnEmployee { get; set; }
        public decimal? PfOrPnEmployer { get; set; }
        public decimal? TotalPfOrPn { get; set; }
        public decimal? LoanDed { get; set; }
        public decimal? CostSharingDed { get; set; }
        public decimal? OtherDed { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? SvAndCrAss { get; set; }
        public decimal? BenifitTax { get; set; }
        public decimal? TotalDed { get; set; }
        public decimal? NetPay { get; set; }
        public string MonthlyAdjustment { get; set; }
        public string MonthlyPaymentSummary { get; set; }
        public bool? Status { get; set; }
    }
}
