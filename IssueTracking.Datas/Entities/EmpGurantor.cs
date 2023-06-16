using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpGurantor
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string GurOrganization { get; set; }
        public Guid? WorkItem { get; set; }
        public decimal? Salary { get; set; }
    }
}
