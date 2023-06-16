using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmpRelations
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Fullname { get; set; }
        public string MotherName { get; set; }
        public long? Dob { get; set; }
        public long? Dom { get; set; }
        public long? CertificateId { get; set; }
        public string Phone { get; set; }
        public long? Type { get; set; }
        public long? PhotoId { get; set; }
        public long? Conditions { get; set; }
        public Guid? WorkItem { get; set; }
    }
}
