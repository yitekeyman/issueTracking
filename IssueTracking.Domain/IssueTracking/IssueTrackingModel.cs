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
        public IssueRaisedSystem RaisedSystem = new IssueRaisedSystem();
        public IList<BasicIssueSolution> IssueSolution = new List<BasicIssueSolution>();
    }

    public class BasicSolutionReturn
    {
        public long Id { get; set; }
        public long IssueTypeId { get; set; }
        public string SolutionQuery { get; set; }
        public string SolutionDescription { get; set; }
        public IList<ResourceModel> SolutionResource = new List<ResourceModel>();
        public IssueTypeReturn IssueType = new IssueTypeReturn();
    }

    public class BasicSolutionModel
    {
        public long Id { get; set; }
        public long IssueTypeId { get; set; }
        public string SolutionQuery { get; set; }
        public string SolutionDescription { get; set; }
        public IList<ResourceModel> SolutionResource = new List<ResourceModel>();
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
        
        public IList<IssueTypeList> IssueType = new List<IssueTypeList>();
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
        public string[] PolicyNo { get; set; }
        public string IssueDescription { get; set; }
        public long? IssuePriority { get; set; }
        
        public IList<ResourceModel> IssueResource = new List<ResourceModel>();
    }

    public class IssueListReturn
    {
       
        public string Id { get; set; }
        public string IssueTitle { get; set; }
        public long? IssueTypeId { get; set; }
        public string OtherIssue { get; set; }
        public string[] PolicyNo { get; set; }
        
        public DepartmentSchemaModel BranchId = new DepartmentSchemaModel();
        public string IssueDescription { get; set; }
        
        public EmployeeModel IssueRequestedBy = new EmployeeModel();
        
        public DateTime IssueRequestedDate { get; set; }
        public string IssuePriority { get; set; }
        public string IssueStatus { get; set; }
        public string Ticket { get; set; }
        public int Participant { get; set; }
        public int Comments { get; set; }
        public int NoOfEdit { get; set; }
        
        public IssueTypeReturn IssueType = new IssueTypeReturn();
       

    }

    public class IssueListReturnModel
    {
        public IList<IssueListReturn> Opened = new List<IssueListReturn>();
        public IList<IssueListReturn> Closed = new List<IssueListReturn>();
        public IList<SideBarStat> UpperSideBarStats = new List<SideBarStat>();
        public IList<SideBarStat> LowerSideBarStats = new List<SideBarStat>();
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
        public string Appellation { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrFatherName { get; set; }
        public string EmpIdNo { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class IssueCommentsModel
    {
        public string Id { get; set; }
        
        public string IssueId { get; set; }
        
        public string IssueComment { get; set; } 

        public string CommentedBy { get; set; }
        
        public DateTime IssueCommentDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        
        public IList<ResourceModel> CommentResource = new List<ResourceModel>();
        
        public IssueStatusType IssueStatus = new IssueStatusType();
       
        public int Status { get; set; }
        
    }
    
    public class MilestonesModel
    {
      public string Id { get; set; }
      
      public string IssueId { get; set; }
      
      public DateTime DueDate { get; set; }
      
      public string Name { get; set; }
      
      public string Description { get; set; }
      
      public string CreatedBy { get; set; }
      
      public DateTime CreatedDate { get; set; }
      
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
}