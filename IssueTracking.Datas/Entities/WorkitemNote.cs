using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class WorkitemNote
    {
        public Guid Id { get; set; }
        public Guid Workitem { get; set; }
        public string Note { get; set; }
        public long Date { get; set; }
        public string Username { get; set; }
        public string ForwardTo { get; set; }

        public virtual Workitem WorkitemNavigation { get; set; }
    }
}
