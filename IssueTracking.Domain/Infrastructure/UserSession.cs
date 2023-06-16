using System;
using System.Collections.Generic;

namespace IssueTracking.Domain.Infrastructure
{
    public class UserSession
    {
        public string Id;
        public string Username { get; set; }
        public long Role { get; set; }
        public string UserId { get; set; }
        public string DepartmentId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastSeen { get; set; }
        public Dictionary<string, object> Content;
    }
}