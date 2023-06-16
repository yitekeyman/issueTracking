using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpEducation
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public long? EductionLavelId { get; set; }
        public string Instituation { get; set; }
        public long? StratDate { get; set; }
        public long? EndDate { get; set; }
        public decimal? Result { get; set; }
        public string Type { get; set; }
        public Guid? WorkItem { get; set; }

        public virtual EductionLevel EductionLavel { get; set; }
    }
}
