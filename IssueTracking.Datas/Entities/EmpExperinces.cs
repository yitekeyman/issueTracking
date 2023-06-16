using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpExperinces
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Position { get; set; }
        public string OrgName { get; set; }
        public decimal? Salarty { get; set; }
        public long? StartDate { get; set; }
        public long? EndDate { get; set; }
        public Guid? WorkItem { get; set; }
    }
}
