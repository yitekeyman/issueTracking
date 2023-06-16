using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Department
    {
        public Department()
        {
            DepartmentSchema = new HashSet<DepartmentSchema>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DepartmentSchema> DepartmentSchema { get; set; }
    }
}
