using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class UnscrConsolidatedList
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Designation { get; set; }
        public string Dob { get; set; }
        public string Pob { get; set; }
        public string GoodQuality { get; set; }
        public string LowQuality { get; set; }
        public string Nationality { get; set; }
        public string PassportNo { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string OtherInfo { get; set; }
        public string ListedOn { get; set; }
        public bool? Status { get; set; }
        public string Type { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
