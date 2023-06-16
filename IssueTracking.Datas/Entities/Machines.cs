using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Machines
    {
        public Machines()
        {
            EmpMachines = new HashSet<EmpMachines>();
            MachineLog = new HashSet<MachineLog>();
        }

        public long Id { get; set; }
        public long? BranchId { get; set; }
        public string Mac { get; set; }
        public string Ip { get; set; }
        public string SerialNo { get; set; }
        public int? Port { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmpMachines> EmpMachines { get; set; }
        public virtual ICollection<MachineLog> MachineLog { get; set; }
    }
}
