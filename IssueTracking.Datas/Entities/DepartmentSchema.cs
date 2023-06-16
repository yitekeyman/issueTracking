using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class DepartmentSchema
    {
        public DepartmentSchema()
        {
            IssuesList = new HashSet<IssuesList>();
        }

        public Guid Id { get; set; }
        public long BranchId { get; set; }
        public long DepartmentId { get; set; }
        public string Tele { get; set; }
        public string Fax { get; set; }
        public string Pobox { get; set; }
        public bool? Status { get; set; }
        public Guid? DepartmentHead { get; set; }

        public virtual Branches Branch { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<IssuesList> IssuesList { get; set; }
    }
}
