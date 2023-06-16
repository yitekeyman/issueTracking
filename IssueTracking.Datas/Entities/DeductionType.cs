using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class DeductionType
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public string Schema { get; set; }
        public string ForWhom { get; set; }
        public int DeductFrom { get; set; }
        public int DeductReasonFor { get; set; }
    }
}
