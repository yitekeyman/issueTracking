using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class HolidayCalander
    {
        public Guid Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int? Year { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
    }
}
