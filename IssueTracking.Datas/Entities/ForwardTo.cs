using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class ForwardTo
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public Guid ForwardFrom { get; set; }
        public Guid ForwardToDept { get; set; }
        public Guid ForwardToEmp { get; set; }
        public string Remark { get; set; }
        public long ForwardDate { get; set; }
        public string IssueResource { get; set; }
    }
}
