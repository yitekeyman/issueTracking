using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class ExternalTrainee
    {
        public Guid Id { get; set; }
        public Guid TrainingId { get; set; }
        public string FullName { get; set; }
        public string WhoIsThis { get; set; }
        public string FromWhere { get; set; }
        public string Phone { get; set; }
        public long Status { get; set; }
        public string Description { get; set; }
        public long Sex { get; set; }
    }
}
