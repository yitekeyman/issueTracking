using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Leaves
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public long? RequestedDate { get; set; }
        public long? ResponseDate { get; set; }
        public string Replay { get; set; }
        public long? StartDate { get; set; }
        public long? EndDate { get; set; }
        public long? Type { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double NoDays { get; set; }
        public string LeaveDateDetails { get; set; }
        public Guid? Workitem { get; set; }
    }
}
