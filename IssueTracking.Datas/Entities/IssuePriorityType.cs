﻿using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssuePriorityType
    {
        public IssuePriorityType()
        {
            IssuesList = new HashSet<IssuesList>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<IssuesList> IssuesList { get; set; }
    }
}
