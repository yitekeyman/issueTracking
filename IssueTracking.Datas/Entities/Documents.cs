using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Documents
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? FileId { get; set; }
        public long? DocumentTypeId { get; set; }
        public byte[] File { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
    }
}
