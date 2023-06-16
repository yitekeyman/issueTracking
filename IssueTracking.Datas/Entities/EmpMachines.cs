using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpMachines
    {
        public Guid Id { get; set; }
        public int? ActionType { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool? Flag { get; set; }
        public long? MachineId { get; set; }
        public string Name { get; set; }
        public long? Timestamp { get; set; }
        public string EmpIdNo { get; set; }
        public bool IsAnalyzed { get; set; }

        public virtual Machines Machine { get; set; }
    }
}
