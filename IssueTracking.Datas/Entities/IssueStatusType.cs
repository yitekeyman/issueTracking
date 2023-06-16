using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueStatusType
    {
        public IssueStatusType()
        {
            IssueComments = new HashSet<IssueComments>();
            IssuesList = new HashSet<IssuesList>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<IssueComments> IssueComments { get; set; }
        public virtual ICollection<IssuesList> IssuesList { get; set; }
    }
}
