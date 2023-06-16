using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class MachineLog
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public long? MachineId { get; set; }
        public long? Time { get; set; }
        public string EmpIdNo { get; set; }
        public int? LogType { get; set; }
        public bool Flag { get; set; }

        public virtual Machines Machine { get; set; }
    }
}
