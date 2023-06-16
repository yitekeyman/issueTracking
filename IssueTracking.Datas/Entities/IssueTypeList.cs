using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueTypeList
    {
        public IssueTypeList()
        {
            BasicIssueSolution = new HashSet<BasicIssueSolution>();
            IssuesList = new HashSet<IssuesList>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? RaisedSystemId { get; set; }

        public virtual ICollection<BasicIssueSolution> BasicIssueSolution { get; set; }
        public virtual ICollection<IssuesList> IssuesList { get; set; }
    }
}
