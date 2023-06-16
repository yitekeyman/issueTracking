using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpProfessionalSkill
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string SkillName { get; set; }
        public decimal? SkillValue { get; set; }
        public Guid? WorkItem { get; set; }
    }
}
