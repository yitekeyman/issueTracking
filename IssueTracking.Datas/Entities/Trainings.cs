using System;
using System.Collections.Generic;

namespace IssueTracking.Datas.Entities
{
    public partial class Trainings
    {
        public Guid Id { get; set; }
        public string TrainingName { get; set; }
        public long StartDate { get; set; }
        public long? EndDate { get; set; }
        public string Description { get; set; }
        public string TrainerName { get; set; }
        public string Location { get; set; }
        public int? Status { get; set; }
        public long Date { get; set; }
        public string Institution { get; set; }
        public int NoTrainee { get; set; }
        public string Department { get; set; }
        public double? NoOfDays { get; set; }
        public int TrainingType { get; set; }
        public int TrainingFor { get; set; }
        public string TrainingCalendar { get; set; }
    }
}
