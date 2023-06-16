using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeDelegation
    {
        public Guid Id { get; set; }
        public Guid DelegatedBy { get; set; }
        public Guid DelegatedTo { get; set; }
        public long Date { get; set; }
        public long DateFrom { get; set; }
        public long DateTo { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }

        public virtual Employee DelegatedByNavigation { get; set; }
        public virtual Employee DelegatedToNavigation { get; set; }
    }
}
