using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Termination
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public long? TerminationDate { get; set; }
        public int? Reason { get; set; }
        public string ReasonDetail { get; set; }
        public bool? ByWilling { get; set; }
    }
}
