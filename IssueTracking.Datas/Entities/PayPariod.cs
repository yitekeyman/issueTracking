using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class PayPariod
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? DateFrom { get; set; }
        public long? DateTo { get; set; }
        public long? PaidOn { get; set; }
        public int? Days { get; set; }
        public int? Hours { get; set; }
        public int? Status { get; set; }
    }
}
