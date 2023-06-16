using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class UserRole
    {
        public long RoleId { get; set; }
        public Guid UserId { get; set; }

        public virtual RoleType Role { get; set; }
        public virtual Account User { get; set; }
    }
}
