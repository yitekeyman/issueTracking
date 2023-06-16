using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class LoanType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Schema { get; set; }
        public string Description { get; set; }
    }
}
