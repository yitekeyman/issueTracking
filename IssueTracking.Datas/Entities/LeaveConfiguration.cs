using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class LeaveConfiguration
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long? Value { get; set; }
    }
}
