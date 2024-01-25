using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueDependancies
    {
        public Guid Id { get; set; }
        public Guid MajorIssue { get; set; }
        public Guid Dependancies { get; set; }
    }
}
