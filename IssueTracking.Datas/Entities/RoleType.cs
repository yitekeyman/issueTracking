using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class RoleType
    {
        public RoleType()
        {
            UserRole = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
