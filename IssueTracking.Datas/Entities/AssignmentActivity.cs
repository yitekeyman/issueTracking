using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class AssignmentActivity
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public string ActivityName { get; set; }
        public Guid? ParentActivity { get; set; }
        public long StartDate { get; set; }
        public long? EndDate { get; set; }
        public double Value { get; set; }
        public int Status { get; set; }
    }
}
