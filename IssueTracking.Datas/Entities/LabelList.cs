using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class LabelList
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
