using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class EmployeeDelete
    {
        public Guid Id { get; set; }
        public Guid OldId { get; set; }
        public string Applelation { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrFatherName { get; set; }
        public string MotherName { get; set; }
        public long? Dob { get; set; }
        public string Pob { get; set; }
        public long? MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string AlternativePhone { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string Title { get; set; }
        public string TransferReason { get; set; }
        public long? PossitionId { get; set; }
        public long? StartDate { get; set; }
        public long? ContractType { get; set; }
        public long? EmployeeStatus { get; set; }
        public long? ManpowerType { get; set; }
        public Guid? DepartmentId { get; set; }
        public long? Level { get; set; }
        public Guid? Superviesor { get; set; }
        public Guid? WorkItem { get; set; }
        public long? Sex { get; set; }
        public string EmpIdNo { get; set; }
        public string Ethnic { get; set; }
        public long? HealthCondition { get; set; }
        public int StaffProfession { get; set; }
        public string JobGrade { get; set; }
    }
}
