using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class AssEmpTraining
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? TrainingId { get; set; }
        public int? Status { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
