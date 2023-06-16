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
        public IssueTypeList IssueType = new IssueTypeList();
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
}