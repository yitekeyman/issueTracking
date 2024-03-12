using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IssueTracking.Datas.Entities
{
    public partial class LIC_HRMSContext : DbContext
    {
        public LIC_HRMSContext()
        {
        }

        public LIC_HRMSContext(DbContextOptions<LIC_HRMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ActionTracker> ActionTracker { get; set; }
        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<AssEmpTraining> AssEmpTraining { get; set; }
        public virtual DbSet<AssignmentActivity> AssignmentActivity { get; set; }
        public virtual DbSet<AssignmentProgress> AssignmentProgress { get; set; }
        public virtual DbSet<Assignments> Assignments { get; set; }
        public virtual DbSet<AttendanceRule> AttendanceRule { get; set; }
        public virtual DbSet<Attendances> Attendances { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<BankList> BankList { get; set; }
        public virtual DbSet<BasicIssueSolution> BasicIssueSolution { get; set; }
        public virtual DbSet<BenefitType> BenefitType { get; set; }
        public virtual DbSet<BranchType> BranchType { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<CompanyProfile> CompanyProfile { get; set; }
        public virtual DbSet<DailyAttendaces> DailyAttendaces { get; set; }
        public virtual DbSet<DeductionType> DeductionType { get; set; }
        public virtual DbSet<DeletedIssuesList> DeletedIssuesList { get; set; }
        public virtual DbSet<Demotion> Demotion { get; set; }
        public virtual DbSet<DemotionDelete> DemotionDelete { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentSchema> DepartmentSchema { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<EductionLevel> EductionLevel { get; set; }
        public virtual DbSet<EmpAnnualLeaves> EmpAnnualLeaves { get; set; }
        public virtual DbSet<EmpAssignments> EmpAssignments { get; set; }
        public virtual DbSet<EmpAttendanceRule> EmpAttendanceRule { get; set; }
        public virtual DbSet<EmpEducation> EmpEducation { get; set; }
        public virtual DbSet<EmpEmergencyContact> EmpEmergencyContact { get; set; }
        public virtual DbSet<EmpExperinces> EmpExperinces { get; set; }
        public virtual DbSet<EmpGurantor> EmpGurantor { get; set; }
        public virtual DbSet<EmpLanguageSkill> EmpLanguageSkill { get; set; }
        public virtual DbSet<EmpLeaveTermination> EmpLeaveTermination { get; set; }
        public virtual DbSet<EmpMachines> EmpMachines { get; set; }
        public virtual DbSet<EmpProfessionalSkill> EmpProfessionalSkill { get; set; }
        public virtual DbSet<EmpRelations> EmpRelations { get; set; }
        public virtual DbSet<EmpTraining> EmpTraining { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeBenefit> EmployeeBenefit { get; set; }
        public virtual DbSet<EmployeeDeduction> EmployeeDeduction { get; set; }
        public virtual DbSet<EmployeeDelegation> EmployeeDelegation { get; set; }
        public virtual DbSet<EmployeeDelete> EmployeeDelete { get; set; }
        public virtual DbSet<EmployeeLoanAmortization> EmployeeLoanAmortization { get; set; }
        public virtual DbSet<EmployeeLoanData> EmployeeLoanData { get; set; }
        public virtual DbSet<EmployeeLoanReturn> EmployeeLoanReturn { get; set; }
        public virtual DbSet<EmployeeOtherDeduction> EmployeeOtherDeduction { get; set; }
        public virtual DbSet<EmployeePensionType> EmployeePensionType { get; set; }
        public virtual DbSet<EmployeeProfile> EmployeeProfile { get; set; }
        public virtual DbSet<ExternalTrainee> ExternalTrainee { get; set; }
        public virtual DbSet<FormulaVariable> FormulaVariable { get; set; }
        public virtual DbSet<ForwardTo> ForwardTo { get; set; }
        public virtual DbSet<HolidayCalander> HolidayCalander { get; set; }
        public virtual DbSet<IssueAssigned> IssueAssigned { get; set; }
        public virtual DbSet<IssueComments> IssueComments { get; set; }
        public virtual DbSet<IssueDependancies> IssueDependancies { get; set; }
        public virtual DbSet<IssueDueDate> IssueDueDate { get; set; }
        public virtual DbSet<IssueMilestones> IssueMilestones { get; set; }
        public virtual DbSet<IssueNotification> IssueNotification { get; set; }
        public virtual DbSet<IssuePriorityType> IssuePriorityType { get; set; }
        public virtual DbSet<IssueRaisedSystem> IssueRaisedSystem { get; set; }
        public virtual DbSet<IssueStatusType> IssueStatusType { get; set; }
        public virtual DbSet<IssueTimeTracker> IssueTimeTracker { get; set; }
        public virtual DbSet<IssueTypeList> IssueTypeList { get; set; }
        public virtual DbSet<IssuesList> IssuesList { get; set; }
        public virtual DbSet<LabelList> LabelList { get; set; }
        public virtual DbSet<Labels> Labels { get; set; }
        public virtual DbSet<LeaveConfiguration> LeaveConfiguration { get; set; }
        public virtual DbSet<Leaves> Leaves { get; set; }
        public virtual DbSet<LoanType> LoanType { get; set; }
        public virtual DbSet<MachineLog> MachineLog { get; set; }
        public virtual DbSet<Machines> Machines { get; set; }
        public virtual DbSet<Milestones> Milestones { get; set; }
        public virtual DbSet<MonthlyAttendance> MonthlyAttendance { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<PayPariod> PayPariod { get; set; }
        public virtual DbSet<PayrollComponent> PayrollComponent { get; set; }
        public virtual DbSet<Possitions> Possitions { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<PromotionDelete> PromotionDelete { get; set; }
        public virtual DbSet<RecruitmentCandidate> RecruitmentCandidate { get; set; }
        public virtual DbSet<Recuitement> Recuitement { get; set; }
        public virtual DbSet<RoleType> RoleType { get; set; }
        public virtual DbSet<SalaryAdjustment> SalaryAdjustment { get; set; }
        public virtual DbSet<SalaryAdjustmentDelete> SalaryAdjustmentDelete { get; set; }
        public virtual DbSet<ShiftAttendanceRule> ShiftAttendanceRule { get; set; }
        public virtual DbSet<ShiftDayOfWeek> ShiftDayOfWeek { get; set; }
        public virtual DbSet<SimpleTask> SimpleTask { get; set; }
        public virtual DbSet<SystemParameter> SystemParameter { get; set; }
        public virtual DbSet<Termination> Termination { get; set; }
        public virtual DbSet<TrainingCost> TrainingCost { get; set; }
        public virtual DbSet<Trainings> Trainings { get; set; }
        public virtual DbSet<Transfer> Transfer { get; set; }
        public virtual DbSet<UserAction> UserAction { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Workflow> Workflow { get; set; }
        public virtual DbSet<WorkflowDocument> WorkflowDocument { get; set; }
        public virtual DbSet<WorkflowType> WorkflowType { get; set; }
        public virtual DbSet<Workitem> Workitem { get; set; }
        public virtual DbSet<WorkitemNote> WorkitemNote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http: //go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=LIC_HRMS;Username=postgres;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account", "administrator");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ_account_username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeprtmentSchemaId).HasColumnName("deprtment_schema_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ActionTracker>(entity =>
            {
                entity.ToTable("action_tracker", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionDate).HasColumnName("action_date");

                entity.Property(e => e.ActionDetails)
                    .HasColumnName("action_details")
                    .HasColumnType("json");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnName("action_type")
                    .HasMaxLength(150);

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("action_type", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AssEmpTraining>(entity =>
            {
                entity.ToTable("ass_emp_training", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TrainingId).HasColumnName("training_id");
            });

            modelBuilder.Entity<AssignmentActivity>(entity =>
            {
                entity.ToTable("assignment_activity", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActivityName)
                    .IsRequired()
                    .HasColumnName("activity_name")
                    .HasMaxLength(5000);

                entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.ParentActivity).HasColumnName("parent_activity");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<AssignmentProgress>(entity =>
            {
                entity.ToTable("assignment_progress", "employee");

                entity.HasIndex(e => e.AssignEmpId)
                    .HasName("IXFK_assignment_progress_employee");

                entity.HasIndex(e => e.AssignmentId)
                    .HasName("IXFK_assignment_progress_assignments");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.AssignEmpId).HasColumnName("assign_emp_id");

                entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ProgressPercent)
                    .HasColumnName("progress_percent")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentProgress)
                    .HasForeignKey(d => d.AssignmentId)
                    .HasConstraintName("FK_assignment_progress_assignments");
            });

            modelBuilder.Entity<Assignments>(entity =>
            {
                entity.ToTable("assignments", "employee");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("IXFK_assignments_department_schema");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignmentName)
                    .HasColumnName("assignment_name")
                    .HasMaxLength(1000);

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<AttendanceRule>(entity =>
            {
                entity.ToTable("attendance_rule", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('employee.\"att_rule_seq\"'::text)::regclass)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Attendances>(entity =>
            {
                entity.ToTable("attendances", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.DocId).HasColumnName("doc_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("audit_log", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('administrator.\"auditlog_seq\"'::text)::regclass)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasMaxLength(50);

                entity.Property(e => e.ActionTypeId).HasColumnName("action_type_id");

                entity.Property(e => e.KeyValues).HasColumnName("key_values");

                entity.Property(e => e.NewValues).HasColumnName("new_values");

                entity.Property(e => e.OldValues).HasColumnName("old_values");

                entity.Property(e => e.TableName)
                    .HasColumnName("table_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BankList>(entity =>
            {
                entity.ToTable("bank_list", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(350);
            });

            modelBuilder.Entity<BasicIssueSolution>(entity =>
            {
                entity.ToTable("basic_issue_solution", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('issue_tracking.\"issue_solution_seq\"'::text)::regclass)");

                entity.Property(e => e.IssueTypeId).HasColumnName("issue_type_id");

                entity.Property(e => e.SolutionDescription).HasColumnName("solution_description");

                entity.Property(e => e.SolutionQuery).HasColumnName("solution_query");

                entity.Property(e => e.SolutionResource)
                    .HasColumnName("solution_resource")
                    .HasColumnType("json");

                entity.HasOne(d => d.IssueType)
                    .WithMany(p => p.BasicIssueSolution)
                    .HasForeignKey(d => d.IssueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("issue_type_list_fk");
            });

            modelBuilder.Entity<BenefitType>(entity =>
            {
                entity.ToTable("benefit_type", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.IsPayrollAdded).HasColumnName("is_payroll_added");

                entity.Property(e => e.Schema)
                    .HasColumnName("schema")
                    .HasColumnType("json");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(150);

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<BranchType>(entity =>
            {
                entity.ToTable("branch_type", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Branches>(entity =>
            {
                entity.ToTable("branches", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('administrator.\"branch_seq\"'::text)::regclass)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.BaranchType).HasColumnName("baranch_type");

                entity.Property(e => e.BraName)
                    .IsRequired()
                    .HasColumnName("bra_name")
                    .HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasColumnName("branch_code");

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Pobox)
                    .HasColumnName("pobox")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(50);

                entity.HasOne(d => d.BaranchTypeNavigation)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.BaranchType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_branches_branch_type");
            });

            modelBuilder.Entity<CompanyProfile>(entity =>
            {
                entity.ToTable("company_profile", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('administrator.\"company_profile_id_seq\"'::text)::regclass)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.ComName)
                    .HasColumnName("com_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LogoFile).HasColumnName("logo_file");

                entity.Property(e => e.LogoMime)
                    .HasColumnName("logo_mime")
                    .HasMaxLength(50);

                entity.Property(e => e.RegistrationNo)
                    .HasColumnName("registration_no")
                    .HasMaxLength(50);

                entity.Property(e => e.TinNo)
                    .HasColumnName("tin_no")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<DailyAttendaces>(entity =>
            {
                entity.ToTable("daily_attendaces", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_daily_attendaces_employee_03");

                entity.HasIndex(e => e.Id)
                    .HasName("IXFK_daily_attendaces_employee_02");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abscent)
                    .HasColumnName("abscent")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Day)
                    .HasColumnName("day")
                    .HasMaxLength(50);

                entity.Property(e => e.EarlyMin).HasColumnName("early_min");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasColumnType("character varying");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.From)
                    .HasColumnName("from")
                    .HasColumnType("character varying");

                entity.Property(e => e.In1)
                    .HasColumnName("in1")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.LateMin).HasColumnName("late_min");

                entity.Property(e => e.LeaveHrs)
                    .HasColumnName("leave_hrs")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Othrs)
                    .HasColumnName("othrs")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Out1)
                    .HasColumnName("out1")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Shift)
                    .HasColumnName("shift")
                    .HasMaxLength(50);

                entity.Property(e => e.WorkHours)
                    .HasColumnName("work_hours")
                    .HasColumnType("time without time zone");
            });

            modelBuilder.Entity<DeductionType>(entity =>
            {
                entity.ToTable("deduction_type", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeductFrom).HasColumnName("deduct_from");

                entity.Property(e => e.DeductReasonFor).HasColumnName("deduct_reason_for");

                entity.Property(e => e.ForWhom)
                    .HasColumnName("for_whom")
                    .HasMaxLength(150);

                entity.Property(e => e.Schema)
                    .HasColumnName("schema")
                    .HasColumnType("json");

                entity.Property(e => e.TypeName)
                    .HasColumnName("type_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<DeletedIssuesList>(entity =>
            {
                entity.ToTable("deleted_issues_list", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Indexs).HasColumnName("indexs");

                entity.Property(e => e.IssueDetails)
                    .HasColumnName("issue_details")
                    .HasColumnType("json");

                entity.Property(e => e.ModifiyedBy).HasColumnName("modifiyed_by");

                entity.Property(e => e.ModifiyedDate).HasColumnName("modifiyed_date");

                entity.Property(e => e.OldIssueId).HasColumnName("old_issue_id");

                entity.HasOne(d => d.ModifiyedByNavigation)
                    .WithMany(p => p.DeletedIssuesList)
                    .HasForeignKey(d => d.ModifiyedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deleted_issue_modifiyer_id_fk");

                entity.HasOne(d => d.OldIssue)
                    .WithMany(p => p.DeletedIssuesList)
                    .HasForeignKey(d => d.OldIssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("deleted_issue_id_fk");
            });

            modelBuilder.Entity<Demotion>(entity =>
            {
                entity.ToTable("demotion", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.DemotionDate).HasColumnName("demotion_date");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.HasSalaryAdjustment).HasColumnName("has_salary_adjustment");

                entity.Property(e => e.HasTransfer)
                    .HasColumnName("has_transfer")
                    .HasColumnType("json");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.Positions).HasColumnName("positions");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<DemotionDelete>(entity =>
            {
                entity.ToTable("demotion_delete", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.DemotionDate).HasColumnName("demotion_date");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.HasSalaryAdjustment).HasColumnName("has_salary_adjustment");

                entity.Property(e => e.HasTransfer)
                    .HasColumnName("has_transfer")
                    .HasColumnType("json");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.NewDemotionId).HasColumnName("new_demotion_id");

                entity.Property(e => e.Positions).HasColumnName("positions");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('administrator.\"deprtment_seq\"'::text)::regclass)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DepartmentSchema>(entity =>
            {
                entity.ToTable("department_schema", "administrator");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fki_FK_department_schema_branches");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fki_FK_department_schema_department");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.DepartmentHead).HasColumnName("department_head");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(50);

                entity.Property(e => e.Pobox)
                    .HasColumnName("pobox")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tele)
                    .HasColumnName("tele")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.DepartmentSchema)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_department_schema_branches");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentSchema)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_department_schema_department");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("document_type", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.ToTable("documents", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.File).HasColumnName("file");

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasMaxLength(50);

                entity.Property(e => e.MimeType)
                    .HasColumnName("mime_type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EductionLevel>(entity =>
            {
                entity.ToTable("eduction_level", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmpAnnualLeaves>(entity =>
            {
                entity.ToTable("emp_annual_leaves", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BudgetYear)
                    .HasColumnName("budget_year")
                    .HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EmpIdNo)
                    .IsRequired()
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Expired)
                    .HasColumnName("expired")
                    .HasDefaultValueSql("0.0");

                entity.Property(e => e.NoDays).HasColumnName("no_days");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(150);

                entity.Property(e => e.Used)
                    .HasColumnName("used")
                    .HasDefaultValueSql("0.0");
            });

            modelBuilder.Entity<EmpAssignments>(entity =>
            {
                entity.ToTable("emp_assignments", "employee");

                entity.HasIndex(e => e.AssignmentId)
                    .HasName("IXFK_emp_assignments_assignments");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_assignments_employee");

                entity.HasIndex(e => e.SubstituteId)
                    .HasName("IXFK_emp_assignments_employee_02");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(150);

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.SubstituteId).HasColumnName("substitute_id");

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.EmpAssignments)
                    .HasForeignKey(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_emp_assignments_assignments");
            });

            modelBuilder.Entity<EmpAttendanceRule>(entity =>
            {
                entity.ToTable("emp_attendance_rule", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_attendance_rule_employee");

                entity.HasIndex(e => e.RuleId)
                    .HasName("IXFK_emp_attendance_rule_attendance_rule");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DefaultRule)
                    .HasColumnName("default_rule")
                    .HasMaxLength(150);

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasColumnType("character varying");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.RuleId).HasColumnName("rule_id");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.EmpAttendanceRule)
                    .HasForeignKey(d => d.RuleId)
                    .HasConstraintName("FK_emp_attendance_rule_attendance_rule");
            });

            modelBuilder.Entity<EmpEducation>(entity =>
            {
                entity.ToTable("emp_education", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_education_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EductionLavelId).HasColumnName("eduction_lavel_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Instituation)
                    .HasColumnName("instituation")
                    .HasMaxLength(250);

                entity.Property(e => e.Result)
                    .HasColumnName("result")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.StratDate).HasColumnName("strat_date");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(250);

                entity.Property(e => e.WorkItem).HasColumnName("workItem");

                entity.HasOne(d => d.EductionLavel)
                    .WithMany(p => p.EmpEducation)
                    .HasForeignKey(d => d.EductionLavelId)
                    .HasConstraintName("FK_emp_education_eduction_level");
            });

            modelBuilder.Entity<EmpEmergencyContact>(entity =>
            {
                entity.ToTable("emp_emergency_contact", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.AlternativePhone)
                    .HasColumnName("alternative_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Relationship)
                    .HasColumnName("relationship")
                    .HasMaxLength(50);

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmpExperinces>(entity =>
            {
                entity.ToTable("emp_experinces", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_experinces_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.OrgName)
                    .HasColumnName("org_name")
                    .HasMaxLength(250);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(250);

                entity.Property(e => e.Salarty)
                    .HasColumnName("salarty")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmpGurantor>(entity =>
            {
                entity.ToTable("emp_gurantor", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_gurantor_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(150);

                entity.Property(e => e.GurOrganization)
                    .HasColumnName("gur_organization")
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("numeric(10,2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmpLanguageSkill>(entity =>
            {
                entity.ToTable("emp_language_skill", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_language_skill_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.LanguageName)
                    .HasColumnName("language_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Listen).HasColumnName("listen");

                entity.Property(e => e.Read).HasColumnName("read");

                entity.Property(e => e.Speak).HasColumnName("speak");

                entity.Property(e => e.WorkItem).HasColumnName("workItem");

                entity.Property(e => e.Write).HasColumnName("write");
            });

            modelBuilder.Entity<EmpLeaveTermination>(entity =>
            {
                entity.ToTable("emp_leave_termination", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AffectedAnnualLeaves).HasColumnName("affected_annual_leaves");

                entity.Property(e => e.AllowedBy)
                    .HasColumnName("allowed_by")
                    .HasMaxLength(150);

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.LeaveId).HasColumnName("leave_id");

                entity.Property(e => e.NewData)
                    .IsRequired()
                    .HasColumnName("new_data")
                    .HasColumnType("json");

                entity.Property(e => e.OldData)
                    .IsRequired()
                    .HasColumnName("old_data")
                    .HasColumnType("json");

                entity.Property(e => e.Reason).HasColumnName("reason");
            });

            modelBuilder.Entity<EmpMachines>(entity =>
            {
                entity.ToTable("emp_machines", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_machines_employee");

                entity.HasIndex(e => e.MachineId)
                    .HasName("IXFK_emp_machines_machines");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionType).HasColumnName("action_type");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasColumnType("character varying");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.IsAnalyzed).HasColumnName("is_analyzed");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.EmpMachines)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_emp_machines_machines");
            });

            modelBuilder.Entity<EmpProfessionalSkill>(entity =>
            {
                entity.ToTable("emp_professional_skill", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_professional_skill_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.SkillName)
                    .HasColumnName("skill_name")
                    .HasMaxLength(50);

                entity.Property(e => e.SkillValue)
                    .HasColumnName("skill_value")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmpRelations>(entity =>
            {
                entity.ToTable("emp_relations", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_relations_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.Conditions).HasColumnName("conditions");

                entity.Property(e => e.Dob).HasColumnName("dob");

                entity.Property(e => e.Dom).HasColumnName("dom");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(150);

                entity.Property(e => e.MotherName)
                    .HasColumnName("mother_name")
                    .HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.PhotoId).HasColumnName("photo_id");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmpTraining>(entity =>
            {
                entity.ToTable("emp_training", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_emp_training_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Instituation)
                    .HasColumnName("instituation")
                    .HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.TrainingId).HasColumnName("training_id");

                entity.Property(e => e.TrainingType)
                    .HasColumnName("training_type")
                    .HasMaxLength(250);

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.AlternativePhone)
                    .HasColumnName("alternative_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Applelation)
                    .HasColumnName("applelation")
                    .HasMaxLength(50);

                entity.Property(e => e.ContractType).HasColumnName("contract_type");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Dob).HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeStatus).HasColumnName("employee_status");

                entity.Property(e => e.Ethnic)
                    .HasColumnName("ethnic")
                    .HasMaxLength(50);

                entity.Property(e => e.FatherName)
                    .HasColumnName("father_name")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.GrFatherName)
                    .HasColumnName("gr_father_name")
                    .HasMaxLength(50);

                entity.Property(e => e.HealthCondition)
                    .HasColumnName("health_condition")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(150);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.ManpowerType).HasColumnName("manpower_type");

                entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");

                entity.Property(e => e.MotherName)
                    .HasColumnName("mother_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Nationality)
                    .HasColumnName("nationality")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Pob)
                    .HasColumnName("pob")
                    .HasMaxLength(50);

                entity.Property(e => e.PossitionId).HasColumnName("possition_id");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.StaffProfession)
                    .HasColumnName("staff_profession")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Superviesor).HasColumnName("superviesor");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.TransferReason)
                    .HasColumnName("transfer_reason")
                    .HasMaxLength(50);

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmployeeBenefit>(entity =>
            {
                entity.ToTable("employee_benefit", "payroll");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_employee_benefit_employee_profile");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BenefitName)
                    .HasColumnName("benefit_name")
                    .HasMaxLength(150);

                entity.Property(e => e.BenefitTypeId).HasColumnName("benefit_type_id");

                entity.Property(e => e.BenefitValue)
                    .HasColumnName("benefit_value")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.IsTaxable).HasColumnName("is_taxable");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<EmployeeDeduction>(entity =>
            {
                entity.ToTable("employee_deduction", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeductionId).HasColumnName("deduction_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<EmployeeDelegation>(entity =>
            {
                entity.ToTable("employee_delegation", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.DelegatedBy).HasColumnName("delegated_by");

                entity.Property(e => e.DelegatedTo).HasColumnName("delegated_to");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Remark).HasColumnName("remark");

                entity.HasOne(d => d.DelegatedByNavigation)
                    .WithMany(p => p.EmployeeDelegationDelegatedByNavigation)
                    .HasForeignKey(d => d.DelegatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_delegated_by_emp_id");

                entity.HasOne(d => d.DelegatedToNavigation)
                    .WithMany(p => p.EmployeeDelegationDelegatedToNavigation)
                    .HasForeignKey(d => d.DelegatedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_delegated_to_emp_id");
            });

            modelBuilder.Entity<EmployeeDelete>(entity =>
            {
                entity.ToTable("employee_delete", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(150);

                entity.Property(e => e.AlternativePhone)
                    .HasColumnName("alternative_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Applelation)
                    .HasColumnName("applelation")
                    .HasMaxLength(50);

                entity.Property(e => e.ContractType).HasColumnName("contract_type");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Dob).HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeStatus).HasColumnName("employee_status");

                entity.Property(e => e.Ethnic)
                    .HasColumnName("ethnic")
                    .HasMaxLength(50);

                entity.Property(e => e.FatherName)
                    .HasColumnName("father_name")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.GrFatherName)
                    .HasColumnName("gr_father_name")
                    .HasMaxLength(50);

                entity.Property(e => e.HealthCondition)
                    .HasColumnName("health_condition")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(150);

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.ManpowerType).HasColumnName("manpower_type");

                entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");

                entity.Property(e => e.MotherName)
                    .HasColumnName("mother_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Nationality)
                    .HasColumnName("nationality")
                    .HasMaxLength(50);

                entity.Property(e => e.OldId).HasColumnName("old_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Pob)
                    .HasColumnName("pob")
                    .HasMaxLength(50);

                entity.Property(e => e.PossitionId).HasColumnName("possition_id");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.StaffProfession)
                    .HasColumnName("staff_profession")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Superviesor).HasColumnName("superviesor");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.TransferReason)
                    .HasColumnName("transfer_reason")
                    .HasMaxLength(50);

                entity.Property(e => e.WorkItem).HasColumnName("workItem");
            });

            modelBuilder.Entity<EmployeeLoanAmortization>(entity =>
            {
                entity.ToTable("employee_loan_amortization", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BeginningBalance)
                    .HasColumnName("beginning_balance ")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.CumulativeInterest)
                    .HasColumnName("cumulative_interest")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndingBalance)
                    .HasColumnName("ending_balance")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.ExtraPayment)
                    .HasColumnName("extra_payment")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Interest)
                    .HasColumnName("interest")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.LoanDataId).HasColumnName("loan_data_id");

                entity.Property(e => e.PaidBy).HasColumnName("paid_by");

                entity.Property(e => e.PaidDate).HasColumnName("paid_date");

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.Principal)
                    .HasColumnName("principal")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.ScheduledPayment)
                    .HasColumnName("scheduled_payment")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPayment)
                    .HasColumnName("total_payment")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<EmployeeLoanData>(entity =>
            {
                entity.ToTable("employee_loan_data", "payroll");

                entity.HasIndex(e => new { e.LoanType, e.Name })
                    .HasName("IXFK_employee_loan_data_loan_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnnualInterestRate)
                    .HasColumnName("annual_interest_rate")
                    .HasColumnType("numeric(10,2)")
                    .HasDefaultValueSql("5");

                entity.Property(e => e.CurrentPayroll)
                    .HasColumnName("current_payroll")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Guarantor).HasColumnName("guarantor");

                entity.Property(e => e.InterestFreeAmount)
                    .HasColumnName("interest_free_amount")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.InterestedAmount)
                    .HasColumnName("interested_amount")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.LoanPeriodInMonth)
                    .HasColumnName("loan_period_in_month")
                    .HasDefaultValueSql("6");

                entity.Property(e => e.LoanType).HasColumnName("loan_type");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.TotalLoanAmount)
                    .HasColumnName("total_loan_amount")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<EmployeeLoanReturn>(entity =>
            {
                entity.ToTable("employee_loan_return", "payroll");

                entity.HasIndex(e => e.LoanDataId)
                    .HasName("IXFK_employee_loan_return_employee_loan_data");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.LoanDataId).HasColumnName("loan_data_id");

                entity.Property(e => e.RastAmount)
                    .HasColumnName("rast_amount")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.ReturnedAmount)
                    .HasColumnName("returned_amount")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<EmployeeOtherDeduction>(entity =>
            {
                entity.ToTable("employee_other_deduction", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.DeductFor).HasColumnName("deduct_for");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasMaxLength(250);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<EmployeePensionType>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.PensionType })
                    .HasName("Pk_employee_pension_type");

                entity.ToTable("employee_pension_type", "payroll");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.PensionType).HasColumnName("pension_type");

                entity.Property(e => e.EmployeePId).HasColumnName("employee_p_id");
            });

            modelBuilder.Entity<EmployeeProfile>(entity =>
            {
                entity.ToTable("employee_profile", "payroll");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_employee_profile_employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Bank)
                    .HasColumnName("bank")
                    .HasMaxLength(150);

                entity.Property(e => e.BankAccount)
                    .HasColumnName("bank_account")
                    .HasMaxLength(50);

                entity.Property(e => e.BankBranch)
                    .HasColumnName("bank_branch")
                    .HasMaxLength(150);

                entity.Property(e => e.CostSharingPayableAmount)
                    .HasColumnName("cost_sharing_payable_amount")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.CostSharingStatus).HasColumnName("cost_sharing_status");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeName)
                    .HasColumnName("employee_name")
                    .HasMaxLength(150);

                entity.Property(e => e.GrossSalary).HasColumnName("gross_salary");

                entity.Property(e => e.PfNumber)
                    .HasColumnName("pf_number")
                    .HasMaxLength(50);

                entity.Property(e => e.Tin)
                    .HasColumnName("tin")
                    .HasMaxLength(50);

                entity.Property(e => e.TotalCostSharingDebt)
                    .HasColumnName("total_cost_sharing_debt")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<ExternalTrainee>(entity =>
            {
                entity.ToTable("external_trainee", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.FromWhere).HasColumnName("from_where");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name")
                    .HasMaxLength(300);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(100);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TrainingId).HasColumnName("training_id");

                entity.Property(e => e.WhoIsThis).HasColumnName("who_is_this");
            });

            modelBuilder.Entity<FormulaVariable>(entity =>
            {
                entity.ToTable("formula_variable", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FormulaVal).HasColumnName("formula_val");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ForwardTo>(entity =>
            {
                entity.ToTable("forward_to", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ForwardDate).HasColumnName("forward_date");

                entity.Property(e => e.ForwardFrom).HasColumnName("forward_from");

                entity.Property(e => e.ForwardToDept).HasColumnName("forward_to_dept");

                entity.Property(e => e.ForwardToEmp).HasColumnName("forward_to_emp");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.IssueResource)
                    .HasColumnName("issue_resource")
                    .HasColumnType("json");

                entity.Property(e => e.Remark).HasColumnName("remark");
            });

            modelBuilder.Entity<HolidayCalander>(entity =>
            {
                entity.ToTable("holiday_calander", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<IssueAssigned>(entity =>
            {
                entity.ToTable("issue_assigned", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignDate).HasColumnName("assign_date");

                entity.Property(e => e.AssignedBy).HasColumnName("assigned_by");

                entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.HasOne(d => d.AssignedByNavigation)
                    .WithMany(p => p.IssueAssignedAssignedByNavigation)
                    .HasForeignKey(d => d.AssignedBy)
                    .HasConstraintName("assign_assigned_by_id_fk");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.IssueAssignedAssignedToNavigation)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("assign_assigned_to_id_fk");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueAssigned)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("assign_issue_id_fk");
            });

            modelBuilder.Entity<IssueComments>(entity =>
            {
                entity.ToTable("issue_comments", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommentDate).HasColumnName("comment_date");

                entity.Property(e => e.CommentResource)
                    .HasColumnName("comment_resource")
                    .HasColumnType("json");

                entity.Property(e => e.CommentedBy).HasColumnName("commented_by");

                entity.Property(e => e.IssueComment).HasColumnName("issue_comment");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.IssueStatus).HasColumnName("issue_status");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CommentedByNavigation)
                    .WithMany(p => p.IssueComments)
                    .HasForeignKey(d => d.CommentedBy)
                    .HasConstraintName("commented_by_fk");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueComments)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("issue_list_id_fk");

                entity.HasOne(d => d.IssueStatusNavigation)
                    .WithMany(p => p.IssueComments)
                    .HasForeignKey(d => d.IssueStatus)
                    .HasConstraintName("comment_issue_status_fk");
            });

            modelBuilder.Entity<IssueDependancies>(entity =>
            {
                entity.ToTable("issue_dependancies", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dependancies).HasColumnName("dependancies");

                entity.Property(e => e.MajorIssue).HasColumnName("major_issue");
            });

            modelBuilder.Entity<IssueDueDate>(entity =>
            {
                entity.ToTable("issue_due_date", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DueDate)
                    .HasColumnName("due_date")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<IssueMilestones>(entity =>
            {
                entity.ToTable("issue_milestones", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.MilestoneId).HasColumnName("milestone_id");
            });

            modelBuilder.Entity<IssueNotification>(entity =>
            {
                entity.ToTable("issue_notification", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.NotificationDate).HasColumnName("notification_date");

                entity.Property(e => e.NotificationDetail).HasColumnName("notification_detail");

                entity.Property(e => e.NotificationFrom).HasColumnName("notification_from");

                entity.Property(e => e.NotificationTitle).HasColumnName("notification_title");

                entity.Property(e => e.NotificationTo).HasColumnName("notification_to");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.IssueNotification)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("notification_issue_id_fk");
            });

            modelBuilder.Entity<IssuePriorityType>(entity =>
            {
                entity.ToTable("issue_priority_type", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('issue_tracking.\"issue_priority_type_seq\"'::text)::regclass)");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<IssueRaisedSystem>(entity =>
            {
                entity.ToTable("issue_raised_system", "issue_tracking");

                entity.HasIndex(e => e.Name)
                    .HasName("issue_raised_system_name_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('issue_tracking.\"issue_raised_system_seq\"'::text)::regclass)");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<IssueStatusType>(entity =>
            {
                entity.ToTable("issue_status_type", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('issue_tracking.\"issue_status_type_seq\"'::text)::regclass)");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<IssueTimeTracker>(entity =>
            {
                entity.ToTable("issue_time_tracker", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(500);

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<IssueTypeList>(entity =>
            {
                entity.ToTable("issue_type_list", "issue_tracking");

                entity.HasIndex(e => e.Name)
                    .HasName("issue_type_list_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('issue_tracking.\"issue_type_list_seq\"'::text)::regclass)");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.RaisedSystemId).HasColumnName("raised_system_id");
            });

            modelBuilder.Entity<IssuesList>(entity =>
            {
                entity.ToTable("issues_list", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.ForwardTo).HasColumnName("forward_to");

                entity.Property(e => e.IssueClosedBy).HasColumnName("issue_closed_by");

                entity.Property(e => e.IssueClosedDate)
                    .HasColumnName("issue_closed_date")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IssueDescription).HasColumnName("issue_description");

                entity.Property(e => e.IssuePriority).HasColumnName("issue_priority");

                entity.Property(e => e.IssueRaisedSluId).HasColumnName("issue_raised_slu_id");

                entity.Property(e => e.IssueRequestedBy).HasColumnName("issue_requested_by");

                entity.Property(e => e.IssueRequestedDate).HasColumnName("issue_requested_date");

                entity.Property(e => e.IssueResource)
                    .HasColumnName("issue_resource")
                    .HasColumnType("json");

                entity.Property(e => e.IssueRespondBy).HasColumnName("issue_respond_by");

                entity.Property(e => e.IssueRespondDate)
                    .HasColumnName("issue_respond_date")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IssueStatus).HasColumnName("issue_status");

                entity.Property(e => e.IssueTitle)
                    .IsRequired()
                    .HasColumnName("issue_title");

                entity.Property(e => e.IssueTypeId).HasColumnName("issue_type_id");

                entity.Property(e => e.OtherIssue).HasColumnName("other_issue");

                entity.Property(e => e.PolicyNo).HasColumnName("policy_no");

                entity.Property(e => e.Ticket).HasColumnName("ticket");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.IssuesList)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("issue_branch_id_fk");

                entity.HasOne(d => d.IssuePriorityNavigation)
                    .WithMany(p => p.IssuesList)
                    .HasForeignKey(d => d.IssuePriority)
                    .HasConstraintName("issue_priority_id_fk");

                entity.HasOne(d => d.IssueRaisedSlu)
                    .WithMany(p => p.IssuesList)
                    .HasForeignKey(d => d.IssueRaisedSluId)
                    .HasConstraintName("issue_raised_slu_id_fk");

                entity.HasOne(d => d.IssueRequestedByNavigation)
                    .WithMany(p => p.IssuesList)
                    .HasForeignKey(d => d.IssueRequestedBy)
                    .HasConstraintName("issue_requested_user_fk");

                entity.HasOne(d => d.IssueStatusNavigation)
                    .WithMany(p => p.IssuesList)
                    .HasForeignKey(d => d.IssueStatus)
                    .HasConstraintName("issue_status_id_fk");

                entity.HasOne(d => d.IssueType)
                    .WithMany(p => p.IssuesList)
                    .HasForeignKey(d => d.IssueTypeId)
                    .HasConstraintName("issue_type_issue_fk");
            });

            modelBuilder.Entity<LabelList>(entity =>
            {
                entity.ToTable("label_list", "issue_tracking");

                entity.HasIndex(e => e.Name)
                    .HasName("label_list_name_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('issue_tracking.\"issue_label_list_seq\"'::text)::regclass)");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(7);

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Labels>(entity =>
            {
                entity.ToTable("labels", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IssueId).HasColumnName("issue_id");

                entity.Property(e => e.LabelId).HasColumnName("label_id");

                entity.Property(e => e.LabeledBy).HasColumnName("labeled_by");

                entity.Property(e => e.LabeledDate).HasColumnName("labeled_date");

                entity.HasOne(d => d.Issue)
                    .WithMany(p => p.Labels)
                    .HasForeignKey(d => d.IssueId)
                    .HasConstraintName("label_issue_id_fk");

                entity.HasOne(d => d.LabeledByNavigation)
                    .WithMany(p => p.Labels)
                    .HasForeignKey(d => d.LabeledBy)
                    .HasConstraintName("label_labeled_ny_fk");
            });

            modelBuilder.Entity<LeaveConfiguration>(entity =>
            {
                entity.ToTable("leave_configuration", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Leaves>(entity =>
            {
                entity.ToTable("leaves", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_leaves_employee");

                entity.HasIndex(e => e.Type)
                    .HasName("IXFK_leaves_leave_configuration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.LeaveDateDetails)
                    .HasColumnName("leave_date_details")
                    .HasColumnType("json");

                entity.Property(e => e.NoDays).HasColumnName("no_days");

                entity.Property(e => e.Replay)
                    .HasColumnName("replay")
                    .HasMaxLength(50);

                entity.Property(e => e.RequestedDate).HasColumnName("requested_date");

                entity.Property(e => e.ResponseDate).HasColumnName("response_date");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Workitem).HasColumnName("workitem");
            });

            modelBuilder.Entity<LoanType>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Name });

                entity.ToTable("loan_type", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('payroll.\"loan_type_seq\"'::text)::regclass)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Schema).HasColumnName("schema");
            });

            modelBuilder.Entity<MachineLog>(entity =>
            {
                entity.ToTable("machine_log", "employee");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("IXFK_machine_log_employee");

                entity.HasIndex(e => e.MachineId)
                    .HasName("IXFK_machine_log_machines");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasColumnType("character varying");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.LogType).HasColumnName("log_type");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineLog)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK_machine_log_machines");
            });

            modelBuilder.Entity<Machines>(entity =>
            {
                entity.ToTable("machines", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('employee.\"machine_seq\"'::text)::regclass)");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(50);

                entity.Property(e => e.Mac)
                    .HasColumnName("mac")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.SerialNo)
                    .HasColumnName("serial_no")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Milestones>(entity =>
            {
                entity.ToTable("milestones", "issue_tracking");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DueDate).HasColumnName("due_date");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Milestones)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("milestones_creater_id_fk");
            });

            modelBuilder.Entity<MonthlyAttendance>(entity =>
            {
                entity.ToTable("monthly_attendance", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abscent)
                    .HasColumnName("abscent")
                    .HasMaxLength(50);

                entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");

                entity.Property(e => e.Days)
                    .HasColumnName("days")
                    .HasMaxLength(50);

                entity.Property(e => e.EarlyMin)
                    .HasColumnName("early_min")
                    .HasMaxLength(50);

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasColumnType("character varying");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Holiday)
                    .HasColumnName("holiday")
                    .HasMaxLength(50);

                entity.Property(e => e.HolidayOt)
                    .HasColumnName("holiday_ot")
                    .HasMaxLength(50);

                entity.Property(e => e.LateMin)
                    .HasColumnName("late_min")
                    .HasMaxLength(50);

                entity.Property(e => e.OffDays)
                    .HasColumnName("off_days")
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.WeekendOt)
                    .HasColumnName("weekend_ot")
                    .HasMaxLength(50);

                entity.Property(e => e.WorkDays)
                    .HasColumnName("work_days")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DataId).HasColumnName("data_id");

                entity.Property(e => e.Massage)
                    .IsRequired()
                    .HasColumnName("massage")
                    .HasMaxLength(500);

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PayPariod>(entity =>
            {
                entity.ToTable("pay_pariod", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('payroll.\"pay_period_seq\"'::text)::regclass)");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.Days).HasColumnName("days");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PaidOn).HasColumnName("paid_on");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<PayrollComponent>(entity =>
            {
                entity.ToTable("payroll_component", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BasicSalary)
                    .HasColumnName("basic_salary")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.BenifitTax)
                    .HasColumnName("benifit_tax")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.CostSharingDed)
                    .HasColumnName("cost_sharing_ded")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.GrossIncome)
                    .HasColumnName("gross_income")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.HardshipAllow)
                    .HasColumnName("hardship_allow")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.IncomeTax)
                    .HasColumnName("income_tax")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.LaborCost)
                    .HasColumnName("labor_cost")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.LoanDed)
                    .HasColumnName("loan_ded")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.MonthlyAdjustment)
                    .HasColumnName("monthly_adjustment")
                    .HasColumnType("json");

                entity.Property(e => e.MonthlyPaymentSummary)
                    .HasColumnName("monthly_payment_summary")
                    .HasColumnType("json");

                entity.Property(e => e.NetPay)
                    .HasColumnName("net_pay")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.NoOfDays)
                    .HasColumnName("no_of_days")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.OtherAllow)
                    .HasColumnName("other_allow")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.OtherDed)
                    .HasColumnName("other_ded")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.OverTime)
                    .HasColumnName("over_time")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.PeriodId).HasColumnName("period_id");

                entity.Property(e => e.PfOrPnEmployee)
                    .HasColumnName("pf_or_pn_employee")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.PfOrPnEmployer)
                    .HasColumnName("pf_or_pn_employer")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.RepereAllow)
                    .HasColumnName("repere_allow")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SvAndCrAss)
                    .HasColumnName("sv_and_cr_ass")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.TaxableIncome)
                    .HasColumnName("taxable_income")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.TaxableTransAllow)
                    .HasColumnName("taxable_trans_allow")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.TotalDed)
                    .HasColumnName("total_ded")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.TotalPfOrPn)
                    .HasColumnName("total_pf_or_pn")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.TransportAllow)
                    .HasColumnName("transport_allow")
                    .HasColumnType("numeric(10,2)");
            });

            modelBuilder.Entity<Possitions>(entity =>
            {
                entity.ToTable("possitions", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("promotion", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EmpIdNo)
                    .IsRequired()
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.HasSalaryAdjustment)
                    .HasColumnName("has_salary_adjustment")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.HasTransfer)
                    .HasColumnName("has_transfer")
                    .HasColumnType("json");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.Positions).HasColumnName("positions");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<PromotionDelete>(entity =>
            {
                entity.ToTable("promotion_delete", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.HasSalaryAdjustment)
                    .HasColumnName("has_salary_adjustment")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.HasTransfer)
                    .HasColumnName("has_transfer")
                    .HasColumnType("json");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.NewPromoId).HasColumnName("new_promo_id");

                entity.Property(e => e.Positions).HasColumnName("positions");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<RecruitmentCandidate>(entity =>
            {
                entity.ToTable("recruitment_candidate", "employee");

                entity.HasIndex(e => e.RecuitmentId)
                    .HasName("IXFK_recruitment_candidate_recuitement");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AcadamicInformation)
                    .HasColumnName("acadamic_information")
                    .HasColumnType("json");

                entity.Property(e => e.Certification)
                    .HasColumnName("certification")
                    .HasMaxLength(50);

                entity.Property(e => e.Experince)
                    .HasColumnName("experince")
                    .HasMaxLength(150);

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasMaxLength(50);

                entity.Property(e => e.InterviewResult).HasColumnName("interview_result");

                entity.Property(e => e.IsSelected).HasColumnName("is_selected");

                entity.Property(e => e.RecuitmentId).HasColumnName("recuitment_id");

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(150);

                entity.Property(e => e.Screening).HasColumnName("screening");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.WrExamResult).HasColumnName("wr_exam_result");

                entity.HasOne(d => d.Recuitment)
                    .WithMany(p => p.RecruitmentCandidate)
                    .HasForeignKey(d => d.RecuitmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_recruitment_candidate_recuitement");
            });

            modelBuilder.Entity<Recuitement>(entity =>
            {
                entity.ToTable("recuitement", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AppEndDate).HasColumnName("app_end_date");

                entity.Property(e => e.AppStartDate).HasColumnName("app_start_date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.InterviewDate).HasColumnName("interview_date");

                entity.Property(e => e.Position).HasMaxLength(150);

                entity.Property(e => e.Requirment).HasColumnName("requirment");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.WrExamDate).HasColumnName("wr_exam_date");
            });

            modelBuilder.Entity<RoleType>(entity =>
            {
                entity.ToTable("role_type", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SalaryAdjustment>(entity =>
            {
                entity.ToTable("salary_adjustment", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EffectiveDate).HasColumnName("effective_date ");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");
            });

            modelBuilder.Entity<SalaryAdjustmentDelete>(entity =>
            {
                entity.ToTable("salary_adjustment_delete", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.EffectiveDate).HasColumnName("effective_date ");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.NewSaId).HasColumnName("new_sa_id");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");
            });

            modelBuilder.Entity<ShiftAttendanceRule>(entity =>
            {
                entity.ToTable("shift_attendance_rule", "employee");

                entity.HasIndex(e => e.AttendanceRuleId)
                    .HasName("IXFK_shift_attendance_rule_attendance_rule");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('employee.\"att_shift_rule_seq\"'::text)::regclass)");

                entity.Property(e => e.AttendanceRuleId).HasColumnName("attendance_rule_id");

                entity.Property(e => e.Dayoff)
                    .HasColumnName("dayoff")
                    .HasColumnType("character varying");

                entity.Property(e => e.EarlyMinsAllowed).HasColumnName("early_mins_allowed");

                entity.Property(e => e.InHour)
                    .HasColumnName("in_hour")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.LateMinsAllowed).HasColumnName("late_mins_allowed");

                entity.Property(e => e.OutHours)
                    .HasColumnName("out_hours")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Shift)
                    .HasColumnName("shift")
                    .HasMaxLength(50);

                entity.HasOne(d => d.AttendanceRule)
                    .WithMany(p => p.ShiftAttendanceRule)
                    .HasForeignKey(d => d.AttendanceRuleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_shift_attendance_rule_attendance_rule");
            });

            modelBuilder.Entity<ShiftDayOfWeek>(entity =>
            {
                entity.ToTable("shift_day_of_week", "employee");

                entity.HasIndex(e => e.ShiftRuleId)
                    .HasName("IXFK_shift_day_of_week_shift_attendance_rule");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasColumnName("day")
                    .HasColumnType("character varying");

                entity.Property(e => e.ShiftRuleId).HasColumnName("shift_rule_id");

                entity.HasOne(d => d.ShiftRule)
                    .WithMany(p => p.ShiftDayOfWeek)
                    .HasForeignKey(d => d.ShiftRuleId)
                    .HasConstraintName("FK_shift_day_of_week_shift_attendance_rule");
            });

            modelBuilder.Entity<SimpleTask>(entity =>
            {
                entity.ToTable("simple_task", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Calendar)
                    .IsRequired()
                    .HasColumnName("calendar")
                    .HasColumnType("json");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");

                entity.Property(e => e.TaskDetail).HasColumnName("task_detail");

                entity.Property(e => e.TaskTitle)
                    .HasColumnName("task_title")
                    .HasMaxLength(1500);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SimpleTaskEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_task_employee_id");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.SimpleTaskSupervisor)
                    .HasForeignKey(d => d.SupervisorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_task_suppervisor_id");
            });

            modelBuilder.Entity<SystemParameter>(entity =>
            {
                entity.ToTable("system_parameter", "payroll");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('payroll.\"sys_par_seq\"'::text)::regclass)");

                entity.Property(e => e.DeductFrom).HasColumnName("deduct_from");

                entity.Property(e => e.DeductReasonFor).HasColumnName("deduct_reason_for");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Schema)
                    .HasColumnName("schema")
                    .HasColumnType("json");

                entity.Property(e => e.SchemaType).HasColumnName("schema_type");
            });

            modelBuilder.Entity<Termination>(entity =>
            {
                entity.ToTable("termination", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ByWilling).HasColumnName("by_willing");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.ReasonDetail).HasColumnName("reason_detail");

                entity.Property(e => e.TerminationDate).HasColumnName("termination_date");
            });

            modelBuilder.Entity<TrainingCost>(entity =>
            {
                entity.ToTable("training_cost", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CostDetails)
                    .IsRequired()
                    .HasColumnName("cost_details")
                    .HasColumnType("json");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.TotalCost).HasColumnName("total_cost");

                entity.Property(e => e.TrainingId).HasColumnName("training_id");
            });

            modelBuilder.Entity<Trainings>(entity =>
            {
                entity.ToTable("trainings", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasColumnName("department")
                    .HasColumnType("json");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.Institution)
                    .HasColumnName("institution ")
                    .HasColumnType("character varying");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("character varying");

                entity.Property(e => e.NoOfDays).HasColumnName("no_of_days");

                entity.Property(e => e.NoTrainee).HasColumnName("no_trainee");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TrainerName)
                    .HasColumnName("trainer_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.TrainingCalendar)
                    .HasColumnName("training_calendar")
                    .HasColumnType("json");

                entity.Property(e => e.TrainingFor)
                    .HasColumnName("training_for")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.TrainingName)
                    .HasColumnName("training_name")
                    .HasMaxLength(5000);

                entity.Property(e => e.TrainingType)
                    .HasColumnName("training_type")
                    .HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("transfer", "employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Benefits)
                    .HasColumnName("benefits")
                    .HasColumnType("json");

                entity.Property(e => e.DeletedSalary)
                    .HasColumnName("deleted_salary")
                    .HasColumnType("json");

                entity.Property(e => e.EmpIdNo)
                    .HasColumnName("emp_id_no")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.HasSalaryAdjustment).HasColumnName("has_salary_adjustment");

                entity.Property(e => e.JobGrade)
                    .HasColumnName("job_grade")
                    .HasMaxLength(50);

                entity.Property(e => e.Position).HasColumnName("position");

                entity.Property(e => e.Reasons)
                    .HasColumnName("reasons")
                    .HasMaxLength(1000);

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.StaffProfession).HasColumnName("staff_profession");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);

                entity.Property(e => e.TransferDate).HasColumnName("transfer_date");

                entity.Property(e => e.TransferFrom).HasColumnName("transfer_from");

                entity.Property(e => e.TransferTo).HasColumnName("transfer_to");
            });

            modelBuilder.Entity<UserAction>(entity =>
            {
                entity.ToTable("user_action", "administrator");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval(('administrator.\"user_action_id_seq\"'::text)::regclass)");

                entity.Property(e => e.ActionTypeId).HasColumnName("action_type_id");

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(150);

                entity.Property(e => e.TimeStamp).HasColumnName("time_stamp");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.UserAction)
                    .HasForeignKey(d => d.ActionTypeId)
                    .HasConstraintName("FK_user_action_action_type");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.UserAction)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_action_account");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });

                entity.ToTable("user_role", "administrator");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_role_type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_account");
            });

            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.ToTable("workflow", "workflow");

                entity.HasIndex(e => e.TypeId)
                    .HasName("IXFK_workflow_workflow_type");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CurrentState).HasColumnName("current_state");

                entity.Property(e => e.CurrentWorkItem).HasColumnName("current_work_item");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Employee)
                    .HasColumnName("employee")
                    .HasMaxLength(150);

                entity.Property(e => e.Initiator)
                    .HasColumnName("initiator")
                    .HasMaxLength(50);

                entity.Property(e => e.OldState).HasColumnName("old_state");

                entity.Property(e => e.TimeStamp).HasColumnName("time_stamp");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Workflow)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_workflow_workflow_type");
            });

            modelBuilder.Entity<WorkflowDocument>(entity =>
            {
                entity.ToTable("workflow_document", "workflow");

                entity.HasIndex(e => e.WorkflowId)
                    .HasName("IXFK_workflow_document_workflow");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContentDesc)
                    .HasColumnName("content_desc")
                    .HasMaxLength(150);

                entity.Property(e => e.ContentType).HasColumnName("content_type");

                entity.Property(e => e.File).HasColumnName("file");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.MimeType)
                    .HasColumnName("mime_type")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.WorkflowDocument)
                    .HasForeignKey(d => d.WorkflowId)
                    .HasConstraintName("FK_workflow_document_workflow");
            });

            modelBuilder.Entity<WorkflowType>(entity =>
            {
                entity.ToTable("workflow_type", "workflow");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Workitem>(entity =>
            {
                entity.ToTable("workitem", "workflow");

                entity.HasIndex(e => e.AssignedRole)
                    .HasName("IXFK_workitem_role_type");

                entity.HasIndex(e => e.AssignedUser)
                    .HasName("IXFK_workitem_account");

                entity.HasIndex(e => e.WorkflowId)
                    .HasName("IXFK_workitem_workflow");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignedRole).HasColumnName("assigned_role");

                entity.Property(e => e.AssignedUser)
                    .HasColumnName("assigned_user")
                    .HasMaxLength(50);

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("json");

                entity.Property(e => e.DataType)
                    .HasColumnName("data_type")
                    .HasColumnType("character varying");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.FromState).HasColumnName("from_state");

                entity.Property(e => e.SeqNo).HasColumnName("seq_no");

                entity.Property(e => e.Trigger).HasColumnName("trigger");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.WorkflowId).HasColumnName("workflow_id");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.Workitem)
                    .HasForeignKey(d => d.WorkflowId)
                    .HasConstraintName("FK_workitem_workflow");
            });

            modelBuilder.Entity<WorkitemNote>(entity =>
            {
                entity.ToTable("workitem_note", "workflow");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.ForwardTo).HasColumnName("forward_to");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnName("note");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");

                entity.Property(e => e.Workitem).HasColumnName("workitem");

                entity.HasOne(d => d.WorkitemNavigation)
                    .WithMany(p => p.WorkitemNote)
                    .HasForeignKey(d => d.Workitem)
                    .HasConstraintName("workitem_note_workitem_fk");
            });

            modelBuilder.HasSequence("auditlog_seq");

            modelBuilder.HasSequence("branch_seq");

            modelBuilder.HasSequence("company_profile_id_seq");

            modelBuilder.HasSequence("deprtment_seq");

            modelBuilder.HasSequence("user_action_id_seq");

            modelBuilder.HasSequence("att_rule_seq");

            modelBuilder.HasSequence("att_shift_rule_seq");

            modelBuilder.HasSequence("machine_seq");

            modelBuilder.HasSequence("issue_label_list_seq");

            modelBuilder.HasSequence("issue_priority_type_seq");

            modelBuilder.HasSequence("issue_raised_system_seq");

            modelBuilder.HasSequence("issue_solution_seq");

            modelBuilder.HasSequence("issue_status_type_seq");

            modelBuilder.HasSequence("issue_type_list_seq");

            modelBuilder.HasSequence("loan_type_seq");

            modelBuilder.HasSequence("pay_period_seq");

            modelBuilder.HasSequence("sys_par_seq");
        }

        public void SaveChanges(string username, UserAction userAction)
        {
            UserAction.Add(userAction);
            base.SaveChanges();
            var auditEntries = OnBeforeSaveChanges(username, userAction.Id);
            base.SaveChanges();
            OnAfterSaveChanges(auditEntries);
        }

        public UserAction SaveChanges(string username, int actionType)
        {
            var userAction = new UserAction
            {
                ActionTypeId = actionType,
                Username = username,
                TimeStamp = DateTime.Now.Ticks
            };
            UserAction.Add(userAction);
            var auditEnries = OnBeforeSaveChanges(username, userAction.Id);
            base.SaveChanges();
            OnAfterSaveChanges(auditEnries);

            return userAction;
        }

        private List<AuditEntry> OnBeforeSaveChanges(string username, long actionId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.Entity is UserAction || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.Relational().TableName,
                    UserName = username,
                    UserAction = actionId
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary) //For auto-generated properties such as id
                    {
                        //get the value after save
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            //Save audit log for all the changes
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
                AuditLog.Add(auditEntry.ToAudit());

            //return those for which we need to get their primary keys for
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private void OnAfterSaveChanges(List<AuditEntry> entries)
        {
            if (entries == null || entries.Count == 0) return;


            foreach (var auditEntry in entries)
            {
                //Get the auto-generated values
                foreach (var prop in auditEntry.TemporaryProperties)
                    if (prop.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    else
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;

                AuditLog.Add(auditEntry.ToAudit());
            }

            base.SaveChanges();
        }

        public Npgsql.NpgsqlCommand CreateReaderCommand(String sql)
        {
            this.Database.OpenConnection();
            var con = (Npgsql.NpgsqlConnection)this.Database.GetDbConnection();

            var cmd = new Npgsql.NpgsqlCommand(sql, con);
            return cmd;
        }

        public System.Data.DataTable GetDataTable(String sql)
        {
            this.Database.OpenConnection();
            var con = (Npgsql.NpgsqlConnection)this.Database.GetDbConnection();
            using (var cmd = new Npgsql.NpgsqlCommand(sql, con))
            using (var adapter = new Npgsql.NpgsqlDataAdapter(cmd))
            {
                var t = new System.Data.DataTable();
                adapter.Fill(t);
                return t;
            }
        }
    }
}