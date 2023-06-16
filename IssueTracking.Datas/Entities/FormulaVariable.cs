using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class FormulaVariable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FormulaVal { get; set; }
    }
}
