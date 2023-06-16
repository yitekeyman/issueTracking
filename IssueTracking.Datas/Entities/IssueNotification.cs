using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class IssueNotification
    {
        public Guid Id { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDetail { get; set; }
        public Guid? NotificationFrom { get; set; }
        public Guid? NotificationTo { get; set; }
        public long? NotificationDate { get; set; }
        public int? Status { get; set; }
        public Guid? IssueId { get; set; }

        public virtual IssuesList Issue { get; set; }
    }
}
