using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class TrainingCost
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public double TotalCost { get; set; }
        public long Date { get; set; }
        public string CostDetails { get; set; }
        public string Description { get; set; }
    }
}
