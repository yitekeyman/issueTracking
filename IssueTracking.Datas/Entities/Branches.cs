using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Branches
    {
        public Branches()
        {
            DepartmentSchema = new HashSet<DepartmentSchema>();
        }

        public long Id { get; set; }
        public string BraName { get; set; }
        public long BaranchType { get; set; }
        public long? BranchCode { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Pobox { get; set; }
        public bool? Status { get; set; }

        public virtual BranchType BaranchTypeNavigation { get; set; }
        public virtual ICollection<DepartmentSchema> DepartmentSchema { get; set; }
    }
}
