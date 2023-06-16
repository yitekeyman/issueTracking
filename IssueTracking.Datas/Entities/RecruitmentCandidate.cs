using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class RecruitmentCandidate
    {
        public Guid Id { get; set; }
        public Guid? RecuitmentId { get; set; }
        public string Experince { get; set; }
        public string Grade { get; set; }
        public string AcadamicInformation { get; set; }
        public string Certification { get; set; }
        public bool? Screening { get; set; }
        public int? InterviewResult { get; set; }
        public int? WrExamResult { get; set; }
        public bool? IsSelected { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }

        public virtual Recuitement Recuitment { get; set; }
    }
}
