using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class BranchType
    {
        public BranchType()
        {
            Branches = new HashSet<Branches>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Branches> Branches { get; set; }
    }
}
