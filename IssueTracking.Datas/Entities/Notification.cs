using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Notification
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Type { get; set; }
        public long Time { get; set; }
        public string Massage { get; set; }
        public Guid? DataId { get; set; }
        public bool State { get; set; }
    }
}
