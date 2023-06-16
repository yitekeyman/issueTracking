using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpLanguageSkill
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string LanguageName { get; set; }
        public bool? Speak { get; set; }
        public bool? Write { get; set; }
        public bool? Read { get; set; }
        public bool? Listen { get; set; }
        public Guid? WorkItem { get; set; }
    }
}
