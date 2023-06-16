using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class SystemParameter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Schema { get; set; }
        public int? SchemaType { get; set; }
        public int? Flag { get; set; }
        public int DeductFrom { get; set; }
        public int DeductReasonFor { get; set; }
    }
}
