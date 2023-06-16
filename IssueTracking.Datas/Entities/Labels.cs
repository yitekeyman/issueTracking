using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Labels
    {
        public Guid Id { get; set; }
        public Guid? IssueId { get; set; }
        public long? LabelId { get; set; }
        public Guid? LabeledBy { get; set; }
        public long? LabeledDate { get; set; }

        public virtual IssuesList Issue { get; set; }
        public virtual Employee LabeledByNavigation { get; set; }
    }
}
