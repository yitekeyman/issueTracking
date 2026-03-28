using System;
using System.Collections.Generic;
using IssueTracking.Datas.Entities;

namespace IssueTracking.Domain.IssueTracking
{
    public class IssueTrackingModel
    {
        public enum UserActionType
        {
            Login = 1,
            Logout = 2,
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class IssueTypeReturn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LookupModel RaisedSystem { get; set; } = new LookupModel();
        public IList<BasicSolutionReturn> IssueSolution { get; set; } = new List<BasicSolutionReturn>();
    }

    
    public class BasicSolutionReturn
    {
        public long Id { get; set; }
        public long IssueTypeId { get; set; }
        public string SolutionQuery { get; set; }
        public string SolutionDescription { get; set; }
        public IList<ResourceModel> SolutionResource { get; set; } = new List<ResourceModel>();
        public IssueTypeReturn IssueType { get; set; } = new IssueTypeReturn();
    }

    public class BasicSolutionModel
    {
        public long Id { get; set; }
        public long IssueTypeId { get; set; }
        public string SolutionQuery { get; set; }
        public string SolutionDescription { get; set; }
        public IList<ResourceModel> SolutionResource { get; set; } = new List<ResourceModel>();
    }

    public class ResourceModel
    {
        public string DocRef { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
        public int Index { get; set; }
    }
    public class IssueRaisedSystemReturn
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public IList<IssueTypeList> IssueType { get; set; } = new List<IssueTypeList>();
    }
    

    public class LITOptions
    {
        public string ResourcePath { get; set; }
    }

    public class IssuesListModel
    {
        public string Id { get; set; }
        public string IssueTitle { get; set; }
        public long? IssueTypeId { get; set; }
        public string OtherIssue { get; set; }
        public string PolicyNo { get; set; }
        public string IssueDescription { get; set; }
        public long? IssuePriority { get; set; }
        public string ForwardTo { get; set; }
        
        public IList<ResourceModel> IssueResource { get; set; }= new List<ResourceModel>();
    }

    public class IssueListReturn
    {
       
        public string Id { get; set; }
        public string IssueTitle { get; set; }
        public long? IssueTypeId { get; set; }
        public string OtherIssue { get; set; }
        public string PolicyNo { get; set; }
        public string BranchId { get; set; }
        public DepartmentSchemaModel ForwardTo { get; set; }= new DepartmentSchemaModel();
        public string IssueDescription { get; set; }

        public EmployeeModel IssueRequestedBy { get; set; }= new EmployeeModel();
        public DateTime IssueRequestedDate { get; set; }
        public EmployeeModel IssueRespondBy { get; set; }= new EmployeeModel();
        public DateTime IssueRespondDate { get; set; }
        public EmployeeModel IssueClosedBy { get; set; }= new EmployeeModel();
        public DateTime IssueClosedDate { get; set; }
        public LookupModel IssuePriority { get; set; }= new LookupModel();
        public LookupModel IssueStatus { get; set; }= new LookupModel();
        public IList<AssignIssueReturnModel> Assigns { get; set; }= new List<AssignIssueReturnModel>();
        public IList<LabelList> Labels { get; set; }= new List<LabelList>();
        public IList<IssueMilestonesReturn> Milestones { get; set; }= new List<IssueMilestonesReturn>();
        public IList<IssueDependenciesReturn> Dependencies { get; set; }= new List<IssueDependenciesReturn>();
        public TimeTrackerReturn TimeTracker { get; set; }= new TimeTrackerReturn();
        public DueDateReturn DueDate { get; set; }= new DueDateReturn();
        public string Ticket { get; set; }
        public IList<EmployeeModel> Participant { get; set; }= new List<EmployeeModel>();
        public IList<IssueCommentReturnModel> Comments { get; set; }= new List<IssueCommentReturnModel>();
        public IList<ActionTrackerReturnModel> ActionTrackers { get; set; }= new List<ActionTrackerReturnModel>();
        public int NoOfEdit { get; set; }
        public IList<ResourceModel> IssueResource { get; set; }= new List<ResourceModel>();
        public IssueTypeReturn IssueType { get; set; }= new IssueTypeReturn();
        public IList<IssueForwardModelReturn> Forwards { get; set; }= new List<IssueForwardModelReturn>();


    }

    public class AssignIssueModel
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public string AssignedTo { get; set; }
    }
    public class AssignIssueReturnModel
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public EmployeeModel AssignedTo { get; set; }= new EmployeeModel();
        public EmployeeModel AssignedBy { get; set; }= new EmployeeModel();
        public DateTime AssignDate { get; set; }
    }
    public class LookupModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class IssueSearchModel
    {
        public string Id { get; set; }
        public string TicketNo { get; set; }
        public string IssueTitle { get; set; }
        public string IssueType { get; set; }
        public string OtherIssue { get; set; }
        public string PolicyNo { get; set; }
        public string IssueDescription { get; set; }
        public string IssuePriority { get; set; }
        public IssueTypeReturn IssueTypes { get; set; }
        public IList<ResourceModel> IssueResource { get; set; }
        public DateTime OpeningDate { get; set; }
        public String OpenedBy { get; set; }
        public string Branch { get; set; }
        public long Status { get; set; }
    }

    public class SearchIssueResult
    {
        public int OpenedIssue { get; set; }
        public int ClosedIssue { get; set; }
        public int PendingIssue { get; set; }
        public int CancelledIssue { get; set; }

        public IList<IssueSearchModel> IssueList { get; set; }= new List<IssueSearchModel>();
    }
    public class IssueListReturnModel
    {
        public IList<IssueListReturn> Opened { get; set; }= new List<IssueListReturn>();
        public IList<IssueListReturn> Closed { get; set; }= new List<IssueListReturn>();
        public IList<SideBarStat> UpperSideBarStats { get; set; }= new List<SideBarStat>();
        public IList<SideBarStat> LowerSideBarStats { get; set; }= new List<SideBarStat>();
    }

    public class DepartmentSchemaModel
    {
        public string Id { get; set; }
        public long BranchId { get; set; }
        public long DepartmentId { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrFatherName { get; set; }
        public string EmpIdNo { get; set; }
        public string Username { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }

    public class PhoneBookSearchParam
    {
        public string EmpIdNo { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
    }

    public class IssueCommentsModel
    {
        public string Id { get; set; }
        
        public string IssueId { get; set; }
        
        public string IssueComment { get; set; }

        public EmployeeModel CommentedBy { get; set; }= new EmployeeModel();
        
        public DateTime IssueCommentDate { get; set; }
        
        public IList<ResourceModel> CommentResource { get; set; }= new List<ResourceModel>();
        
    }

    public class ActionTrackerReturnModel
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public DateTime ActionDate { get; set; }
        public EmployeeModel UserId { get; set; }
        public string ActionType { get; set; }
        public int ActionTypeId { get; set; }
        public string ActionDetails { get; set; }
        public string Remark { get; set; }
    }

    public class IssueCommentReturnModel
    {
        public IssueCommentsModel Comment { get; set; }= new IssueCommentsModel();
        public IList<ActionTrackerReturnModel> Actions { get; set; }= new List<ActionTrackerReturnModel>();
    }
    

    public class IssueFilterParameter
    {
        public long Priority { get; set; }
        public long RaisedSystem { get; set; }
        public long IssueType { get; set; }
        public long Sort { get; set; }
        public string Branch { get; set; }
        public string UserId { get; set; }
    }

    public class SideBarStat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Stat { get; set; }
    }

    public class QueryParams
    {
        public long State { get; set; }
        public string Query { get; set; }
        public string Assignee { get; set; }
        public string Branch { get; set; }
        public long Type { get; set; }
        public long Sort { get; set; }
        public long IssueType { get; set; }
        public long Priority { get; set; }
    }

    public class PatchActionModel
    {
        public IList<string> CaseList { get; set; }= new List<string>();
        public string Remark { get; set; }
    }

    public class ActionTrackerModel
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public IssueSearchModel Issue { get; set; }= new IssueSearchModel();
        public DateTime ActionDate { get; set; }
        public EmployeeModel UserId { get; set; }
        public string ActionType { get; set; }
        public int ActionTypeId { get; set; }
        public string ActionDetails { get; set; }
        public string Remark { get; set; }
    }

    public class DashboardStat
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        public int Open { get; set; }
        public int Closed { get; set; }
    }

    public class RaisedSystem
    {
        public DashboardStat System { get; set; }= new DashboardStat();
        public IList<DashboardStat> IssueType { get; set; }= new List<DashboardStat>();
    }
    public class DashboardModel
    {
        public IList<ActionTrackerModel> Actions { get; set; } = new List<ActionTrackerModel>();
        public IList<RaisedSystem> RaisedSystems { get; set; } = new List<RaisedSystem>();
        public int Open { get; set; }
        public int Closed { get; set; }
        public int Total { get; set; }
    }

    public class MileStonesModel
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MileStonesModelReturn
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeModel CreatedBy { get; set; }= new EmployeeModel();
        public DateTime CreatedDate { get; set; }
    }

    public class IssueMilestonesReturn
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public string MilestoneId { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeModel AddedBy { get; set; }= new EmployeeModel();
        public DateTime AddedOn { get; set; }
    }

    public class IssueDependenciesReturn
    {
        public string Id { get; set; }
        public IssueSearchModel DependentIssue { get; set; }= new IssueSearchModel();
        public EmployeeModel AddedBy { get; set; }= new EmployeeModel();
        public DateTime AddedOn { get; set; }
    }

    public class TimeTrackerReturn
    {
        public TimeTrackerModel MyActiveTask { get; set; }= new TimeTrackerModel();
        public IList<TimeTrackerModel> ActiveTask { get; set; }= new List<TimeTrackerModel>();
        public IList<TimeTrackerModel> EndedTask { get; set; }= new List<TimeTrackerModel>();
    }
    public class TimeTrackerModel
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public DateTime StartTime { get; set;}
        public DateTime EndTime { get; set;}
        public string Status { get; set;}
        public EmployeeModel Owner { get; set; }= new EmployeeModel();
    }

    public class ActionResultReturn
    {
        public string IssueId { get; set; }
        public string TicketNo { get; set; }
        public string IssueTitle { get; set; }
        public string PolicyNo { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public List<string> Dependencies { get; set; }
    }

    public class DueDateReturn
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public EmployeeModel SetBy { get; set; }= new EmployeeModel();
    }
    
    public class NotificationModel
    {
        public string Id { get; set; }
        public int TitleId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDetail { get; set; }
        public EmployeeModel NotificationFrom { get; set; }= new EmployeeModel();
        public string NotificationTo { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool Status { get; set; }
        public string IssueId { get; set; }
        
    }

    public enum NotificationType
    {
        MyCases = 1
    }

    public class IssueNotificationReturnModel
    {
        public int UnreadNotification { get; set; }
        public int ReadNotification { get; set; }
        public IList<NotificationModel> Notifications { get; set; }= new List<NotificationModel>();
    }

    public class IssueForwardModel
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public string ForwardToDept { get; set; }
        public string ForwardToEmp { get; set; }
        public string Remark { get; set; }
        public IList<ResourceModel> IssueResource { get; set; }= new List<ResourceModel>();

    }
    
    public class IssueForwardModelReturn
    {
        public string Id { get; set; }
        public string IssueId { get; set; }
        public EmployeeModel ForwardFrom { get; set; }= new EmployeeModel();
        public DepartmentSchemaModel ForwardToDept { get; set; }= new DepartmentSchemaModel();
        public EmployeeModel ForwardToEmp { get; set; }= new EmployeeModel();
        public string Remark { get; set; }
        public DateTime ForwardDate { get; set; }
        public IList<ResourceModel> IssueResource { get; set; }= new List<ResourceModel>();

    }
}