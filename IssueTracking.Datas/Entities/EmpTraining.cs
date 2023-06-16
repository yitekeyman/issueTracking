using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpTraining
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string TrainingType { get; set; }
        public string Instituation { get; set; }
        public long? StartDate { get; set; }
        public long? EndDate { get; set; }
        public Guid? TrainingId { get; set; }
        public Guid? WorkItem { get; set; }
    }
}
