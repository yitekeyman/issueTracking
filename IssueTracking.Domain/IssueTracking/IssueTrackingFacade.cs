﻿using System;
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
        LookupModel GetIssueRaisedSystemById(UserSession session,long id);
        IList<LookupModel> GetAllIssueRaisedSystems(UserSession session);
        IssueRaisedSystemReturn GetRaisedSystemById(UserSession session,long id);
        IList<IssueRaisedSystemReturn> GetAllRaisedSystems(UserSession session);
        void EditIssueType(UserSession session,IssueTypeList model);
        IssueTypeReturn GetIssueTypeById(UserSession session,long id);
        IList<IssueTypeReturn> GetAllIssueType(UserSession session, long systemRaisedId);

        void EditBasicIssueSolution(UserSession session,BasicSolutionModel model);
        BasicSolutionReturn GetBasicSolutionById(UserSession session,long id);
        IList<BasicSolutionReturn> GetBasicSolutionByIssueType(UserSession session,long id);
        IList<BasicSolutionReturn> GetAllBasicSolution(UserSession session);
        string GetResourceDoc(UserSession session, string fileName, string mimeType);
        string AddIssue(UserSession session, IssuesListModel model);
        void EditIssue(UserSession session, IssuesListModel model);
        void AddIssueComment(UserSession session, IssueCommentsModel model);
        void EditIssueComment(UserSession session, IssueCommentsModel model);
        IList<IssueCommentReturnModel> GetAllIssueComments(UserSession session, string issueId);
        void DeleteIssueComment(UserSession session, string commentId);
        SearchIssueResult GetAllIssues(UserSession session, QueryParams model);
       //IList<IssuesListModel> GetsAllIssues(UserSession session);
        IssueListReturn GetIssueById(UserSession session, Guid id);
        
       // IList<IssueListReturn> GetIssueByStatus(UserSession session,IssueFilterParameter model, long status);
        IList<DepartmentSchemaModel> GetAllBranch(UserSession session);
        IList<EmployeeModel> GetAllEmployee(UserSession session);
        IList<EmployeeModel> GetAllEmployeeByBranchId(UserSession session,string id);
        void AssignIssue(UserSession session,AssignIssueModel model);
        void CloseIssue(UserSession session, string issueId, string remark);
        void ReopenIssue(UserSession session, string issueId,string remark);
        string PatchCloseIssue(UserSession session,PatchActionModel model);
        string PatchReopenIssue(UserSession session, PatchActionModel model);
        DashboardModel GetDashboard(UserSession session, string deptId);
        void EditMileStone(UserSession session, MileStonesModel model);
        IList<MileStonesModelReturn> GetAllMilestones(UserSession session);
        MileStonesModelReturn GetMilestoneById(UserSession session, string id);
        void DeleteMilestone(UserSession session, string id);
        void AddMilestoneToIssue(UserSession session, string issueId, string milestoneId);
        void RemoveMilestoneFromIssue(UserSession session, string id);
        void RemoveAssignFromIssue(UserSession session, string id);
        IList<IssueSearchModel> GetAllDependents(UserSession session, string issueId);
        void AddDependencyToIssue(UserSession session, string issueId, string depeId);
        void RemoveDependencyFromIssue(UserSession session, string id);
        void StartTask(UserSession session, string issueId);
        void EndTask(UserSession session, string id);
        void AddDueDate(UserSession session,string issueId, DateTime dueDate);
        IList<EmployeeModel> GetPhoneBook(UserSession session, PhoneBookSearchParam model);
        IList<DepartmentSchemaModel> GetHeadOfficeDept(UserSession session);
        void ForwardIssue(UserSession session, IssueForwardModel model);
        IssueNotificationReturnModel GetNotification(UserSession session);
        void MarkReadNotification(UserSession session,string notId);
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

        public LookupModel GetIssueRaisedSystemById(UserSession session, long id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetIssueRaisedSystemById(id);
        }

        public IList<LookupModel> GetAllIssueRaisedSystems(UserSession session)
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

        public IList<IssueTypeReturn> GetAllIssueType(UserSession session,long systemRaisedId)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssueType(systemRaisedId);
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
        
        public IssueListReturn GetIssueById(UserSession session, Guid id)
        {
           _issueTrackingService.SetSession(session);
           return _issueTrackingService.GetIssueById(id);
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

        public void AddIssueComment(UserSession session, IssueCommentsModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.AddIssueComment(model);
        }

        public void EditIssueComment(UserSession session, IssueCommentsModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EditIssueComment(model);
        }

        public IList<IssueCommentReturnModel> GetAllIssueComments(UserSession session, string issueId)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssueComments(issueId);
        }

        public void DeleteIssueComment(UserSession session, string commentId)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.DeleteIssueComment(commentId);
        }
        
/*
        public IList<IssueListReturn> GetIssueByStatus(UserSession session, IssueFilterParameter model, long status)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetIssueByStatus(model,status);
        } */
        
        
        public string AddIssue(UserSession session, IssuesListModel model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.AddIssue(model);
        }

        public void EditIssue(UserSession session, IssuesListModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EditIssue(model);
        }

        public SearchIssueResult GetAllIssues(UserSession session, QueryParams model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllIssues(model);
        } 
        /*
        public IList<IssuesListModel> GetsAllIssues(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetsAllIssues();
        } */

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

        public void AssignIssue(UserSession session, AssignIssueModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.AssignIssue(model);
        }

        public void CloseIssue(UserSession session, string issueId, string remark)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.CloseIssue(issueId,remark);
        }

        public void ReopenIssue(UserSession session, string issueId, string remark)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.ReopenIssue(issueId,remark);
        }

        public string PatchCloseIssue(UserSession session, PatchActionModel model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.PatchCloseIssue(model);
        }

        public string PatchReopenIssue(UserSession session, PatchActionModel model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.PatchReopenIssue(model);
        }

        public DashboardModel GetDashboard(UserSession session, string deptId)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetDashboard(deptId);
        }

        public void EditMileStone(UserSession session, MileStonesModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EditMileStone(model);
        }

        public IList<MileStonesModelReturn> GetAllMilestones(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllMilestones();
        }

        public MileStonesModelReturn GetMilestoneById(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetMilestoneById(id);
        }

        public void DeleteMilestone(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.DeleteMilestone(id);
        }

        public void AddMilestoneToIssue(UserSession session, string issueId, string milestoneId)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.AddMilestoneToIssue(issueId, milestoneId);
        }

        public void RemoveMilestoneFromIssue(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.RemoveMilestoneFromIssue(id);
        }

        public void RemoveAssignFromIssue(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.RemoveAssignFromIssue(id);
        }

        public IList<IssueSearchModel> GetAllDependents(UserSession session, string issueId)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetAllDependents(issueId);
        }

        public void AddDependencyToIssue(UserSession session, string issueId, string depeId)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.AddDependencyToIssue(issueId,depeId);
        }

        public void RemoveDependencyFromIssue(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.RemoveDependencyFromIssue(id);
        }

        public void StartTask(UserSession session, string issueId)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.StartTask(issueId);
        }

        public void EndTask(UserSession session, string id)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.EndTask(id);
        }
        public void AddDueDate(UserSession session,string issueId, DateTime dueDate){
            _issueTrackingService.SetSession(session);
            _issueTrackingService.AddDueDate(issueId, dueDate);
        }

        public IList<EmployeeModel> GetPhoneBook(UserSession session, PhoneBookSearchParam model)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetPhoneBook(model);
        }

        public IList<DepartmentSchemaModel> GetHeadOfficeDept(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetHeadOfficeDept();
        }
        
        public IssueNotificationReturnModel GetNotification(UserSession session)
        {
            _issueTrackingService.SetSession(session);
            return _issueTrackingService.GetNotification();
        }

        public void MarkReadNotification(UserSession session, string notId)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.MarkReadNotification(notId);
        }

        public void ForwardIssue(UserSession session, IssueForwardModel model)
        {
            _issueTrackingService.SetSession(session);
            _issueTrackingService.ForwardIssue(model);
        }
    }
}
