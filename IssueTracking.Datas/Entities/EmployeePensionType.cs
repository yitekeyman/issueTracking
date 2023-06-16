using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeePensionType
    {
        public Guid EmployeeId { get; set; }
        public Guid? EmployeePId { get; set; }
        public long PensionType { get; set; }
    }
}
