using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class BenefitType
    {
        public string Type { get; set; }
        public decimal? Value { get; set; }
        public Guid Id { get; set; }
        public string Schema { get; set; }
        public bool? IsPayrollAdded { get; set; }
        public int? Flag { get; set; }
    }
}
