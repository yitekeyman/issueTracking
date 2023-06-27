using System.Collections.Generic;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;

namespace IssueTracking.Domain.IssueTracking
{
    public interface IIssueTrackingFacade
    {
        UserSession Login(UserSession session,LoginModel model); 
        IssuePriorityType GetPriorityTypeById(UserSession session,long id);
        IList<IssuePriorityType> GetAllPriorityTypes(UserSession session);
        IList<IssueStatusType> GetAllIssueStatusTypes(UserSession session);
        IssueRaisedSystem GetIssueRaisedSystemById(UserSession session,long id);
        IList<IssueRaisedSystem> GetAllIssueRaisedSystems(UserSession session);
        IssueRaisedSystemReturn GetRaisedSystemById(UserSession session,long id);
        IList<IssueRaisedSystemReturn> GetAllRaisedSystems(UserSession session);
        void EditIssueType(UserSession session,IssueTypeList model);
        IssueTypeReturn GetIssueTypeById(UserSession session,long id);
        IList<IssueTypeReturn> GetAllIssueType(UserSession session);

        void EditBasicIssueSolution(UserSession session,BasicSolutionModel model);
        BasicSolutionReturn GetBasicSolutionById(UserSession session,long id);
        IList<BasicSolutionReturn> GetBasicSolutionByIssueType(UserSession session,long id);
        IList<BasicSolutionReturn> GetAllBasicSolution(UserSession session);
        string GetResourceDoc(UserSession session, string fileName, string mimeType);
        void AddIssue(UserSession session, IssuesListModel model);
        void EditIssue(UserSession session, IssuesListModel model);
        IssueListReturnModel GetAllIssues(UserSession session, IssueFilterParameter model);
        IList<IssueListReturn> GetIssueByStatus(UserSession session,IssueFilterParameter model, long status);
        IList<DepartmentSchemaModel> GetAllBranch(UserSession session);
        IList<EmployeeModel> GetAllEmployee(UserSession session);
        IList<EmployeeModel> GetAllEmployeeByBranchId(UserSession session,string id);
    }
    public class IssueTrackingFacade:IIssueTrackingFacade
    {
        private readonly IIssueTrackingService _issueTrackingService;
        public IssueTrackingFacade(IIssueTrackingService issueTrackingService)
        {
            _issueTrackingService = issueTrackingService;
        }

        public UserSession Login(UserSession session, LoginModel model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.Login(model);
        }

        public IssuePriorityType GetPriorityTypeById(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetPriorityTypeById(id);
        }

        public IList<IssuePriorityType> GetAllPriorityTypes(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllPriorityTypes();
        }

        public IList<IssueStatusType> GetAllIssueStatusTypes(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssueStatusTypes();
        }

        public IssueRaisedSystem GetIssueRaisedSystemById(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetIssueRaisedSystemById(id);
        }

        public IList<IssueRaisedSystem> GetAllIssueRaisedSystems(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssueRaisedSystems();
        }

        public IssueRaisedSystemReturn GetRaisedSystemById(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetRaisedSystemById(id);
        }

        public IList<IssueRaisedSystemReturn> GetAllRaisedSystems(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllRaisedSystems();
        }

        public void EditIssueType(UserSession session, IssueTypeList model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EditIssueType(model);
        }

        public IssueTypeReturn GetIssueTypeById(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetIssueTypeById(id);
        }

        public IList<IssueTypeReturn> GetAllIssueType(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssueType();
        }

        public void EditBasicIssueSolution(UserSession session, BasicSolutionModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EditBasicIssueSolution(model);
        }

        public BasicSolutionReturn GetBasicSolutionById(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetBasicSolutionById(id);
        }

        public IList<BasicSolutionReturn> GetBasicSolutionByIssueType(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetBasicSolutionByIssueType(id);
        }

        public IList<BasicSolutionReturn> GetAllBasicSolution(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllBasicSolution();
        }

        public string GetResourceDoc(UserSession session, string fileName, string mimeType)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetResourceDoc(fileName,mimeType);
        }

        public void AddIssue(UserSession session, IssuesListModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.AddIssue(model);
        }

        public void EditIssue(UserSession session, IssuesListModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EditIssue(model);
        }

        public IssueListReturnModel GetAllIssues(UserSession session, IssueFilterParameter model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssues(model);
        }

        public IList<IssueListReturn> GetIssueByStatus(UserSession session, IssueFilterParameter model, long status)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetIssueByStatus(model,status);
        }

        public IList<DepartmentSchemaModel> GetAllBranch(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllBranch();
        }

        public IList<EmployeeModel> GetAllEmployee(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllEmployee();
        }

        public IList<EmployeeModel> GetAllEmployeeByBranchId(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllEmployeeByBranchId(id);
        }
    }
}