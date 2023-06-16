using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class ActionType
    {
        public ActionType()
        {
            UserAction = new HashSet<UserAction>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserAction> UserAction { get; set; }
    }
}
