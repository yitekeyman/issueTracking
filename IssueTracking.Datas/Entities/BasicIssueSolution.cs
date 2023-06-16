using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class BasicIssueSolution
    {
        public long Id { get; set; }
        public long IssueTypeId { get; set; }
        public string SolutionQuery { get; set; }
        public string SolutionDescription { get; set; }
        public string SolutionResource { get; set; }

        public virtual IssueTypeList IssueType { get; set; }
    }
}
