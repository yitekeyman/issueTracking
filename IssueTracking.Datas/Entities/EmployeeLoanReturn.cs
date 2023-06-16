using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeLoanReturn
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal? ReturnedAmount { get; set; }
        public decimal? RastAmount { get; set; }
        public long? Date { get; set; }
        public Guid? LoanDataId { get; set; }
    }
}
