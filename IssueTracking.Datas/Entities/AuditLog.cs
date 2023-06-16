using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class AuditLog
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public long ActionTypeId { get; set; }
        public long Timestamp { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string TableName { get; set; }
        public string AccountId { get; set; }
    }
}
