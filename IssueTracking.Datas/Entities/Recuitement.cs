using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Recuitement
    {
        public Recuitement()
        {
            RecruitmentCandidate = new HashSet<RecruitmentCandidate>();
        }

        public Guid Id { get; set; }
        public string Position { get; set; }
        public long? AppStartDate { get; set; }
        public long? AppEndDate { get; set; }
        public long? InterviewDate { get; set; }
        public long? WrExamDate { get; set; }
        public string Requirment { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<RecruitmentCandidate> RecruitmentCandidate { get; set; }
    }
}
