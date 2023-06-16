using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class CompanyProfile
    {
        public long Id { get; set; }
        public string ComName { get; set; }
        public string Address { get; set; }
        public byte[] LogoFile { get; set; }
        public string LogoMime { get; set; }
        public string RegistrationNo { get; set; }
        public string TinNo { get; set; }
    }
}
