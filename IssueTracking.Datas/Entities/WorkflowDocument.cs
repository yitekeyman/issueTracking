using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class WorkflowDocument
    {
        public Guid Id { get; set; }
        public Guid? WorkflowId { get; set; }
        public long? ContentType { get; set; }
        public string ContentDesc { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public int? Index { get; set; }
        public string MimeType { get; set; }

        public virtual Workflow Workflow { get; set; }
    }
}
