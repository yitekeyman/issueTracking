using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Attendances
    {
        public Guid Id { get; set; }
        public long DateFrom { get; set; }
        public long? DateTo { get; set; }
        public string Type { get; set; }
        public Guid? DocId { get; set; }
    }
}
