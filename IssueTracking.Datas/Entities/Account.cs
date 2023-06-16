using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Account
    {
        public Account()
        {
            UserAction = new HashSet<UserAction>();
            UserRole = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool? Status { get; set; }
        public Guid? DeprtmentSchemaId { get; set; }

        public virtual ICollection<UserAction> UserAction { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
