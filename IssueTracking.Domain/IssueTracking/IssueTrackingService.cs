using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp.Extensions;

namespace IssueTracking.Domain.IssueTracking
{
    public interface IIssueTrackingService
    {
        void SetSession(UserSession session);
        UserSession Login(LoginModel model);
        IssuePriorityType GetPriorityTypeById(long id);
        IList<IssuePriorityType> GetAllPriorityTypes();
        IList<IssueStatusType> GetAllIssueStatusTypes();
        LookupModel GetIssueRaisedSystemById(long id);
        IList<LookupModel> GetAllIssueRaisedSystems();
        IssueRaisedSystemReturn GetRaisedSystemById(long id);
        IList<IssueRaisedSystemReturn> GetAllRaisedSystems();
        void EditIssueType(IssueTypeList model);
        IssueTypeReturn GetIssueTypeById(long id);
        IList<IssueTypeReturn> GetAllIssueType(long systemRaisedId);

        void EditBasicIssueSolution(BasicSolutionModel model);
        BasicSolutionReturn GetBasicSolutionById(long id);
        IssueListReturn GetIssueByTitle(string IssueTitle);
        IList<BasicSolutionReturn> GetBasicSolutionByIssueType(long id);
        IList<BasicSolutionReturn> GetAllBasicSolution();
        string GetResourceDoc(string fileName, string mimeType);
        string AddIssue(IssuesListModel model);
        void EditIssue(IssuesListModel model);
        SearchIssueResult GetAllIssues(QueryParams model);

        //IList<IssuesListModel> GetsAllIssues();
        IList<IssueListReturn> GetIssueByStatus(IssueFilterParameter parameter, long status);

        IssueListReturn GetIssueById(Guid id);
        //IssueSearchModel GetIssueById(Guid id);

        void AddIssueComment(IssueCommentsModel model);
        void EditIssueComment(IssueCommentsModel model);
        IList<IssueCommentReturnModel> GetAllIssueComments(string issueId);
        void DeleteIssueComment(string commentId);

        IList<DepartmentSchemaModel> GetAllBranch();
        IList<EmployeeModel> GetAllEmployee();
        IList<EmployeeModel> GetAllEmployeeByBranchId(string id);
        void AssignIssue(AssignIssueModel model);
        void CloseIssue(string issueId, string remark);
        void ReopenIssue(string issueId, string remark);
        string PatchCloseIssue(PatchActionModel model);
        string PatchReopenIssue(PatchActionModel model);
        DashboardModel GetDashboard(string deptId);
        void EditMileStone(MileStonesModel model);
        IList<MileStonesModelReturn> GetAllMilestones();
        MileStonesModelReturn GetMilestoneById(string id);
        void DeleteMilestone(string id);
        void AddMilestoneToIssue(string issueId, string milestoneId);
        void RemoveMilestoneFromIssue(string id);
        void RemoveAssignFromIssue(string id);
        IList<IssueSearchModel> GetAllDependents(string issueId);
        void AddDependencyToIssue(string issueId, string depeId);
        void RemoveDependencyFromIssue(string id);
        void StartTask(string issueId);
        void EndTask(string id);
        void AddDueDate(string issueId, DateTime dueDate);
        void DeleteIssueDueDate(string issueId);
        IList<EmployeeModel> GetPhoneBook(PhoneBookSearchParam model);
        IList<DepartmentSchemaModel> GetHeadOfficeDept();
        IssueNotificationReturnModel GetNotification(Boolean status);
        void MarkReadNotification(string notId);
        void ForwardIssue(IssueForwardModel model);
        int GetUnReadNotification();
        string PatchMakeReadNotification(PatchActionModel model);
        void CancelIssue(string issueId, string remark);
    }

    public class IssueTrackingService : IIssueTrackingService
    {
        private readonly IUserActionService _iUserActionService;
        private readonly LIC_HRMSContext _context;
        private UserSession _session;

        private string fileLocation;
        private Regex mimeTypeRegx = new Regex("(.+)/(.+)");

        public IssueTrackingService(LIC_HRMSContext context, IUserActionService userActionService,
            IOptions<LITOptions> resourceFile)
        {
            _iUserActionService = userActionService;
            _context = context;
            fileLocation = resourceFile.Value.ResourcePath;
        }

        public void SetSession(UserSession session)
        {
            _session = session;
        }

        public UserSession Login(LoginModel model)
        {
            var hashPassword = model.Password.Hash();
            try
            {
                var dbUser = _context.Account.First(u =>
                    u.Username.ToLower().Equals(model.Username.ToLower()) && u.Password.Equals(hashPassword));

                if (dbUser.Status == false)
                    throw new AccessDeniedException("User Account Deactivated");
                var userSession = new UserSession()
                {
                    DepartmentId = dbUser.DeprtmentSchemaId.ToString(),
                    UserId = dbUser.EmployeeId.ToString(),
                    Username = dbUser.Username
                };

                var userRole = _context.UserRole.FirstOrDefault(r => r.UserId == dbUser.Id && r.RoleId == 1);
                if (userRole != null)
                {
                    userSession.Role = 1;
                }
                else
                {
                    userSession.Role = 6;
                }

                _iUserActionService.AddUserAction(
                    new UserSession
                        { Username = dbUser.Username },
                    IssueTrackingModel.UserActionType.Login);


                return userSession;
            }
            catch (InvalidOperationException e)
            {
                Console.Error.WriteLine(e);
                throw new AccessDeniedException("Invalid Username or Password, Please correct your information");
            }
        }

        public IssuePriorityType GetPriorityTypeById(long id)
        {
            return _context.IssuePriorityType.First(i => i.Id == id);
        }

        public IList<IssuePriorityType> GetAllPriorityTypes()
        {
            return _context.IssuePriorityType.OrderBy(e => e.Id).ToList();
        }

        public IList<IssueStatusType> GetAllIssueStatusTypes()
        {
            return _context.IssueStatusType.OrderBy(e => e.Id).ToList();
        }

        public LookupModel GetIssueRaisedSystemById(long id)
        {
            var ret = _context.IssueRaisedSystem.First(i => i.Id == id);
            return new LookupModel() { Id = ret.Id, Name = ret.Description };
        }

        public IList<LookupModel> GetAllIssueRaisedSystems()
        {
            var ret = new List<LookupModel>();
            var raisedSystem = _context.IssueRaisedSystem.OrderBy(e => e.Name).ToList();
            foreach (var sys in raisedSystem)
            {
                ret.Add(new LookupModel() { Id = sys.Id, Name = sys.Name, Description = sys.Description });
            }

            return ret;
        }

        public IssueRaisedSystemReturn GetRaisedSystemById(long id)
        {
            var issueRaisedSystem = new IssueRaisedSystemReturn();
            var raisedSystem = _context.IssueRaisedSystem.FirstOrDefault(e => e.Id == id);
            if (raisedSystem != null)
            {
                issueRaisedSystem.Id = raisedSystem.Id;
                issueRaisedSystem.Name = raisedSystem.Name;
                issueRaisedSystem.Description = raisedSystem.Description;
                issueRaisedSystem.IssueType =
                    _context.IssueTypeList.Where(e => e.RaisedSystemId == raisedSystem.Id).ToList();
            }

            return issueRaisedSystem;
        }

        public IList<IssueRaisedSystemReturn> GetAllRaisedSystems()
        {
            var issueRaisedSystemList = new List<IssueRaisedSystemReturn>();
            var raisedSystem = _context.IssueRaisedSystem.OrderBy(e => e.Name).ToList();
            foreach (var rs in raisedSystem)
            {
                issueRaisedSystemList.Add(GetRaisedSystemById(rs.Id));
            }

            return issueRaisedSystemList;
        }

        public void EditIssueType(IssueTypeList model)
        {
            if (model.Id > 0)
            {
                var issueType = _context.IssueTypeList.FirstOrDefault(i => i.Id == model.Id);
                if (issueType != null)
                {
                    issueType.Id = model.Id;
                    issueType.Name = model.Name;
                    issueType.Description = model.Description;
                    issueType.RaisedSystemId = model.RaisedSystemId;

                    _context.IssueTypeList.Update(issueType);
                    _context.SaveChanges();
                }
                else
                {
                    throw new AccessDeniedException("Sorry, I can't find any issue type with id");
                }
            }
            else
            {
                var issueType = new IssueTypeList()
                {
                    Name = model.Name,
                    Description = model.Description,
                    RaisedSystemId = model.RaisedSystemId
                };
                _context.IssueTypeList.Add(issueType);
                _context.SaveChanges();
            }
        }

        public IssueTypeReturn GetIssueTypeById(long id)
        {
            var issueTypes = new IssueTypeReturn();
            var issues = _context.IssueTypeList.FirstOrDefault(i => i.Id == id);
            if (issues != null)
            {
                issueTypes.Id = issues.Id;
                issueTypes.Name = issues.Name;
                issueTypes.Description = issues.Description;
                var solution = _context.BasicIssueSolution.Where(e => e.IssueTypeId == issues.Id).ToList();
                var raisedSystem = _context.IssueRaisedSystem.FirstOrDefault(e => e.Id == issues.RaisedSystemId);
                if (solution.Count > 0)
                {
                    foreach (var sol in solution)
                    {
                        var sl = new BasicSolutionReturn()
                        {
                            Id = sol.Id,
                            SolutionDescription = sol.SolutionDescription,
                            SolutionQuery = sol.SolutionQuery,
                        };
                        issueTypes.IssueSolution.Add(sl);
                    }
                }

                if (raisedSystem != null)
                {
                    issueTypes.RaisedSystem.Id = raisedSystem.Id;
                    issueTypes.RaisedSystem.Name = raisedSystem.Name;
                    issueTypes.RaisedSystem.Description = raisedSystem.Description;
                }
            }

            return issueTypes;
        }

        public IList<IssueTypeReturn> GetAllIssueType(long systemRaisedId)
        {
            var issueTypeList = new List<IssueTypeReturn>();

            var issues = _context.IssueTypeList.OrderBy(e => e.RaisedSystemId).ThenBy(e => e.Name).ToList();
            if (systemRaisedId > -1)
                issues = _context.IssueTypeList.Where(e => e.RaisedSystemId == systemRaisedId).OrderBy(e => e.Name)
                    .ToList();
            foreach (var issue in issues)
            {
                var issueType = GetIssueTypeById(issue.Id);
                if (issueType != null)
                {
                    issueTypeList.Add(issueType);
                }
            }

            return issueTypeList;
        }

        public void EditBasicIssueSolution(BasicSolutionModel model)
        {
            var resourceModels = new List<ResourceModel>();
            if (model.Id > 0)
            {
                var solution = _context.BasicIssueSolution.FirstOrDefault(i => i.Id == model.Id);
                if (solution != null)
                {
                    solution.Id = model.Id;
                    solution.IssueTypeId = model.IssueTypeId;
                    solution.SolutionQuery = model.SolutionQuery;
                    solution.SolutionDescription = model.SolutionDescription;

                    if (model.SolutionResource.Count > 0)
                    {
                        var index = 0;
                        foreach (var res in model.SolutionResource)
                        {
                            index++;
                            if (string.IsNullOrEmpty(res.DocRef))
                            {
                                var resource = new ResourceModel()
                                {
                                    DocRef = Guid.NewGuid().ToString(),
                                    MimeType = res.MimeType,
                                    FileName = res.FileName,
                                    Data = "",
                                    Index = index
                                };
                                SaveResource(resource.DocRef, resource.MimeType, res.Data);
                                resourceModels.Add(resource);
                            }
                            else
                            {
                                res.Data = "";
                                res.Index = index;
                                resourceModels.Add(res);
                            }
                        }

                        solution.SolutionResource = JsonConvert.SerializeObject(resourceModels);
                    }

                    _context.BasicIssueSolution.Update(solution);
                    _context.SaveChanges();
                }
                else
                {
                    throw new AccessDeniedException("Sorry, I can't find any issue solution with this id");
                }
            }
            else
            {
                var solution = new BasicIssueSolution()
                {
                    IssueTypeId = model.IssueTypeId,
                    SolutionQuery = model.SolutionQuery,
                    SolutionDescription = model.SolutionDescription,
                };
                if (model.SolutionResource.Count > 0)
                {
                    var index = 0;
                    foreach (var res in model.SolutionResource)
                    {
                        var resource = new ResourceModel()
                        {
                            DocRef = Guid.NewGuid().ToString(),
                            FileName = res.FileName,
                            MimeType = res.MimeType,
                            Data = "",
                            Index = index++
                        };
                        SaveResource(resource.DocRef, resource.MimeType, res.Data);
                        resourceModels.Add(resource);
                    }

                    solution.SolutionResource = JsonConvert.SerializeObject(resourceModels);
                }

                _context.BasicIssueSolution.Add(solution);
                _context.SaveChanges();
            }
        }

        public BasicSolutionReturn GetBasicSolutionById(long id)
        {
            var basicSolution = new BasicSolutionReturn();
            var solution = _context.BasicIssueSolution.FirstOrDefault(e => e.Id == id);
            if (solution != null)
            {
                basicSolution.Id = solution.Id;
                basicSolution.IssueTypeId = solution.IssueTypeId;
                basicSolution.SolutionDescription = solution.SolutionDescription;
                basicSolution.SolutionQuery = solution.SolutionQuery;
                basicSolution.IssueType = GetIssueTypeById(solution.IssueTypeId);
                if (!string.IsNullOrEmpty(solution.SolutionResource))
                {
                    var imageResource = JsonConvert.DeserializeObject<IList<ResourceModel>>(solution.SolutionResource);
                    foreach (var res in imageResource)
                    {
                        var data = GetResourceDoc(res.DocRef, res.MimeType);
                        if (!string.IsNullOrEmpty(data))
                        {
                            res.Data = data;
                            basicSolution.SolutionResource.Add(res);
                        }
                    }
                }
            }

            return basicSolution;
        }

        public IList<BasicSolutionReturn> GetBasicSolutionByIssueType(long id)
        {
            var basicSolution = new List<BasicSolutionReturn>();
            var solutions = _context.BasicIssueSolution.Where(e => e.IssueTypeId == id).ToList();
            foreach (var solution in solutions)
            {
                var bs = GetBasicSolutionById(solution.Id);
                if (bs != null && bs.Id > 0)
                {
                    basicSolution.Add(bs);
                }
            }

            return basicSolution;
        }

        public IList<BasicSolutionReturn> GetAllBasicSolution()
        {
            var basicSolutions = new List<BasicSolutionReturn>();
            var solutions = _context.BasicIssueSolution.OrderBy(i => i.Id).ToList();
            foreach (var solution in solutions)
            {
                var bs = GetBasicSolutionById(solution.Id);
                if (bs != null && bs.Id > 0)
                {
                    basicSolutions.Add(bs);
                }
            }

            return basicSolutions;
        }

        public IssueListReturn GetIssueByTitle(string issueTitle)
        {
            var issueList = new IssueListReturn();
            var issue = _context.IssuesList.FirstOrDefault(e => e.IssueTitle.Contains(issueTitle));
            if (issue != null)
            {
                issueList = GetIssueById(issue.Id);
            }

            return issueList;
        }

        public string AddIssue(IssuesListModel model)
        {
            var count = (_context.IssuesList.Count() + 1).ToString();
            var pt = "000000";
            var ticket = pt.Substring(0, pt.Length - count.Length) + count;
            var solutionId = _context.BasicIssueSolution.FirstOrDefault(e => e.IssueTypeId == model.IssueTypeId);
            var issue = new IssuesList()
            {
                Id = Guid.NewGuid(),
                IssueTitle = model.IssueTitle,
                IssueTypeId = model.IssueTypeId ?? 0,
                OtherIssue = model.OtherIssue,
                BranchId = Guid.Parse((ReadOnlySpan<char>)_session.DepartmentId),
                IssueDescription = model.IssueDescription,
                PolicyNo = model.PolicyNo,
                IssueRequestedBy = Guid.Parse(_session.UserId),
                IssueRequestedDate = DateTime.Now.Ticks,
                //IssueRespondBy = Guid.Parse(_session.UserId),
                //IssueRespondDate = new DateTime().Ticks,
                //IssueClosedBy =  Guid.Parse(_session.UserId),
                //IssueClosedDate = new DateTime().Ticks,
                IssuePriority = model.IssuePriority,
                IssueStatus = 1,
                Ticket = ticket,
                ForwardTo = Guid.Parse(model.ForwardTo)
            };
            if (solutionId != null)
            {
                issue.IssueRaisedSluId = solutionId.Id;
            }

            if (!model.ForwardTo.Equals("f48cb514-8e36-4a87-a2e0-49042c096c99"))
            {
                issue.IssueStatus = 3;
            }

            if (model.IssueResource.Count > 0)
            {
                var index = 0;
                var resourceModels = new List<ResourceModel>();
                foreach (var res in model.IssueResource)
                {
                    var resource = new ResourceModel()
                    {
                        DocRef = Guid.NewGuid().ToString(),
                        FileName = res.FileName,
                        MimeType = res.MimeType,
                        Data = "",
                        Index = index++
                    };
                    SaveResource(resource.DocRef, resource.MimeType, res.Data);
                    resourceModels.Add(resource);
                }

                issue.IssueResource = JsonConvert.SerializeObject(resourceModels);
            }

            _context.IssuesList.Add(issue);
            _context.SaveChanges();
            CreateActionTrackers(issue.Id.ToString(), "Created Issue", null, null);
            SetNotification(issue.Id, "Created Issue", issue.ForwardTo, null, false);
            return issue.Id.ToString();
        }

        public void EditIssue(IssuesListModel model)
        {
            var index = _context.DeletedIssuesList.Count(e => e.OldIssueId == Guid.Parse(model.Id)) + 1;
            var oldIssue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(model.Id));
            var actionType = "Edited Issue";

            if (oldIssue != null)
            {
                var deletedIssue = new DeletedIssuesList()
                {
                    Id = Guid.NewGuid(),
                    OldIssueId = oldIssue.Id,
                    IssueDetails = JsonConvert.SerializeObject(oldIssue),
                    ModifiyedBy = Guid.Parse(_session.UserId),
                    ModifiyedDate = new DateTime().Ticks,
                    Indexs = index
                };
                var solutionId = _context.BasicIssueSolution.FirstOrDefault(e => e.IssueTypeId == model.IssueTypeId);
                if (solutionId != null)
                {
                    oldIssue.IssueRaisedSluId = solutionId.Id;
                }
                else
                {
                    oldIssue.IssueRaisedSluId = null;
                }

                oldIssue.IssueTypeId = model.IssueTypeId ?? 0;
                oldIssue.OtherIssue = model.OtherIssue;
                oldIssue.IssueTitle = model.IssueTitle;
                oldIssue.IssueDescription = model.IssueDescription;
                oldIssue.IssuePriority = model.IssuePriority;
                oldIssue.PolicyNo = model.PolicyNo;
                oldIssue.ForwardTo = Guid.Parse(model.ForwardTo);

                if (model.IssueResource.Count > 0)
                {
                    var resourceModels = new List<ResourceModel>();
                    var index1 = 0;
                    foreach (var res in model.IssueResource)
                    {
                        index1++;
                        if (string.IsNullOrEmpty(res.DocRef))
                        {
                            var resource = new ResourceModel()
                            {
                                DocRef = Guid.NewGuid().ToString(),
                                MimeType = res.MimeType,
                                FileName = res.FileName,
                                Data = "",
                                Index = index1
                            };
                            SaveResource(resource.DocRef, resource.MimeType, res.Data);
                            resourceModels.Add(resource);
                        }
                        else
                        {
                            res.Data = "";
                            res.Index = index1;
                            resourceModels.Add(res);
                        }
                    }

                    oldIssue.IssueResource = JsonConvert.SerializeObject(resourceModels);
                }

                _context.DeletedIssuesList.Add(deletedIssue);
                _context.IssuesList.Update(oldIssue);
                _context.SaveChanges();
                CreateActionTrackers(model.Id, actionType, null, "Issue Edited");
                SetNotification(Guid.Parse(model.Id), actionType, Guid.Parse(model.ForwardTo), null, true);
            }
            else
            {
                throw new AccessDeniedException("Sorry, I can't find any issue registered with this title of '" +
                                                model.IssueTitle + "'");
            }
        }

        public IssueListReturn GetIssueById(Guid id)
        {
            var issueList = new IssueListReturn();
            var issue = _context.IssuesList.FirstOrDefault(e => e.Id == id);
            if (issue != null)
            {
                issueList.Id = issue.Id.ToString();
                issueList.IssueTitle = issue.IssueTitle;
                issueList.IssueTypeId = issue.IssueTypeId;
                issueList.OtherIssue = issue.OtherIssue;
                issueList.PolicyNo = issue.PolicyNo;
                var branch = GetDepartment(issue.BranchId);
                issueList.BranchId = branch.BranchName;
                if (branch.BranchId == 10)
                {
                    issueList.BranchId = string.Format("{0}({1})", branch.BranchName, branch.DepartmentName);
                }

                issueList.IssueDescription = issue.IssueDescription;
                issueList.IssueRequestedBy = null;
                if (issue.IssueRequestedBy != Guid.Empty)
                    issueList.IssueRequestedBy = GetEmployee(issue.IssueRequestedBy);
                issueList.IssueRequestedDate = new DateTime(issue.IssueRequestedDate ?? 0);
                issueList.IssueRespondBy = null;
                if (issue.IssueRespondBy != Guid.Empty)
                    issueList.IssueRespondBy = GetEmployee(issue.IssueRespondBy);
                issueList.IssueRespondDate = new DateTime(issue.IssueRespondDate ?? 0);
                issueList.IssueClosedBy = null;
                if (issue.IssueClosedBy != Guid.Empty)
                    issueList.IssueRespondBy = GetEmployee(issue.IssueClosedBy);

                issueList.IssueClosedDate = new DateTime(issue.IssueClosedDate ?? 0);
                var priority = _context.IssuePriorityType.First(e => e.Id == issue.IssuePriority);
                issueList.IssuePriority = new LookupModel()
                    { Id = priority.Id, Name = priority.Name, Description = priority.Description };
                var status = _context.IssueStatusType.First(e => e.Id == issue.IssueStatus);
                issueList.IssueStatus = new LookupModel()
                    { Id = status.Id, Name = status.Name, Description = status.Description };
                issueList.Ticket = issue.Ticket;
                issueList.Participant = GetParticipant(issue.Id);
                issueList.Assigns = GetAssigned(issue.Id);
                issueList.Labels = GetIssueLabelsList(issue.Id);
                issueList.Comments = GetAllIssueComments(issue.Id.ToString());
                issueList.NoOfEdit = _context.DeletedIssuesList.Count(e => e.OldIssueId == issue.Id);
                issueList.IssueType = GetIssueTypeById(issue.IssueTypeId ?? 0);
                if (issueList.IssueTypeId == 0)
                    issueList.IssueType.RaisedSystem = GetRaisedSystem(int.Parse(issue.OtherIssue));
                issueList.Milestones = GetissueMilestones(issue.Id.ToString());
                issueList.Dependencies = GetAllIssueDependencies(issue.Id.ToString());
                issueList.TimeTracker = BuildTimeTracker(issue.Id);
                issueList.DueDate = GetDueDateReturn(issue.Id);
                issueList.ForwardTo = GetDepartment(issue.ForwardTo);
                issueList.Forwards = GetAllIssueForward(issue.Id);
                var actionList = _context.ActionTracker.Where(c => c.IssueId == issue.Id)
                    .OrderBy(c => c.ActionDate).ToList();
                foreach (var action in actionList)
                {
                    if (!action.ActionType.Equals("Edited Comment on Issue"))
                    {
                        issueList.ActionTrackers.Add(GetActionTrackerById(action.Id));
                    }
                }

                if (!string.IsNullOrEmpty(issue.IssueResource))
                {
                    var imageResource = JsonConvert.DeserializeObject<IList<ResourceModel>>(issue.IssueResource);
                    foreach (var res in imageResource)
                    {
                        var data = GetResourceDoc(res.DocRef, res.MimeType);
                        if (!string.IsNullOrEmpty(data))
                        {
                            res.Data = data;
                            issueList.IssueResource.Add(res);
                        }
                    }
                }
            }

            return issueList;
        }

        public SearchIssueResult GetAllIssues(QueryParams model)
        {
            var ret = new SearchIssueResult();
            var issuesModel = new List<IssueSearchModel>();
            IList<IssuesList> issues = new List<IssuesList>();
            // var q =
            //     "SELECT * FROM issue_tracking.issues_list il inner join issue_tracking.labels l ON il.id==l.issue_id inner join issue_tracking.issue_assigned ia on ia.issue_id==il.id inner join issue_tracking.milestones ml on ml.issue_id==il.id WHERE il.issue_status=1 AND (il.issue_description LIKE '%%' OR il.issue_title LIKE '%%' OR il.ticket LIKE '%%' OR il.policy_no LIKE '%%') AND ia.assigned_to='' AND il.issue_requested_by='' AND l.id='' AND il.branch_id='' AND ml.id='' Order by il.ticket asc";

            var query = "SELECT il.* FROM issue_tracking.issues_list il ";

            if (!string.IsNullOrEmpty(model.Assignee) || model.Type == 1)
                query = string.Format("{0} inner join issue_tracking.issue_assigned ia on ia.issue_id=il.id", query);
            query = string.Format("{0} WHERE il.issue_status={1}", query, model.State);

            if (!string.IsNullOrEmpty(model.Query))
                query = string.Format(
                    "{0}  AND (il.issue_description LIKE '%{1}%' OR il.issue_title LIKE '%{1}%' OR il.ticket LIKE '%{1}%' OR il.policy_no LIKE '%{1}%')",
                    query, model.Query);
            if (model.IssueType > -1)
                query = string.Format("{0} AND il.issue_type_id={1}", query, model.IssueType);

            if (model.Priority > 0)
                query = string.Format("{0} AND il.issue_priority={1}", query, model.Priority);

            if (model.Type == 1)
            {
                query = String.Format("{0} AND ia.assigned_to='{1}'", query, _session.UserId);
            }
            else if (model.Type == 2)
            {
                query = String.Format("{0} AND il.issue_requested_by='{1}'", query, _session.UserId);
            }
            else if (!string.IsNullOrEmpty(model.Assignee))
            {
                query = string.Format("{0} AND ia.assigned_to='{1}'", query, model.Assignee);
            }

            if (!string.IsNullOrEmpty(model.Branch))
                query = String.Format("{0} AND il.branch_id='{1}'", query, model.Branch);
            if (model.Sort == 1)
            {
                query = String.Format("{0} Order by il.ticket DESC", query);
            }
            else
            {
                query = String.Format("{0} Order by il.ticket ASC", query);
            }

            issues = _context.IssuesList.FromSql(query).ToList();
            foreach (var iss in issues)
            {
                var department = GetDepartment(iss.BranchId).DepartmentName;
                if (GetDepartment(iss.BranchId).BranchId != 10)
                    department = GetDepartment(iss.BranchId).BranchName;

                var issl = new IssueSearchModel()
                {
                    Id = iss.Id.ToString(),
                    TicketNo = iss.Ticket,
                    IssueTitle = iss.IssueTitle,
                    IssueType = _context.IssueTypeList.First(e => e.Id == iss.IssueTypeId).Name,
                    OtherIssue = iss.OtherIssue,
                    PolicyNo = iss.PolicyNo,
                    IssueDescription = iss.IssueDescription,
                    IssuePriority = _context.IssuePriorityType.First(e => e.Id == iss.IssuePriority).Name,
                    OpenedBy = GetEmployee(iss.IssueRequestedBy).Username,
                    OpeningDate = new DateTime(iss.IssueRequestedDate ?? 0),
                    Branch = department,
                    Status = iss.IssueStatus ?? 1,
                    //IssueResource = JsonConvert.DeserializeObject<List<ResourceModel>>(iss.IssueResource),
                };
                issuesModel.Add(issl);
            }

            ret.IssueList = issuesModel;
            if (IsItStaffLoggedIn())
            {
                ret.OpenedIssue = _context.IssuesList.Count(i => i.IssueStatus == 1);
                ret.ClosedIssue = _context.IssuesList.Count(i => i.IssueStatus == 2);
                ret.PendingIssue = _context.IssuesList.Count(i => i.IssueStatus == 3);
                ret.CancelledIssue = _context.IssuesList.Count(i => i.IssueStatus == 4);
            }
            else
            {
                ret.OpenedIssue = _context.IssuesList.Count(i =>
                    i.IssueStatus == 1 && i.BranchId == Guid.Parse(_session.DepartmentId));
                ret.ClosedIssue = _context.IssuesList.Count(i =>
                    i.IssueStatus == 2 && i.BranchId == Guid.Parse(_session.DepartmentId));
                ret.PendingIssue = _context.IssuesList.Count(i =>
                    i.IssueStatus == 3 && (i.BranchId == Guid.Parse(_session.DepartmentId) ||
                                           i.ForwardTo == Guid.Parse(_session.DepartmentId)));
                ret.CancelledIssue = _context.IssuesList.Count(i =>
                    i.IssueStatus == 4 && i.BranchId == Guid.Parse(_session.DepartmentId));
            }

            return ret;
        }

        public IList<IssueListReturn> GetIssueByStatus(IssueFilterParameter parameter, long status)
        {
            var ret = new List<IssueListReturn>();
            var issues = new List<IssuesList>();
            var queryableIssue = _context.IssuesList.Where(e =>
                e.IssueStatus == status &&
                (string.IsNullOrEmpty(parameter.UserId) || Guid.Parse(parameter.UserId) == e.IssueRequestedBy) &&
                (parameter.Priority == 0 || parameter.Priority == e.IssuePriority) &&
                (string.IsNullOrEmpty(parameter.Branch) || Guid.Parse(parameter.Branch) == e.BranchId));

            if (parameter.RaisedSystem == 0)
            {
                if (parameter.Sort == 1)
                {
                    issues = queryableIssue.OrderByDescending(e => e.IssueRequestedDate).ToList();
                }
                else if (parameter.Sort == 2)
                {
                    issues = queryableIssue.OrderBy(e => e.IssueRequestedDate).ToList();
                }
                else if (parameter.Sort == 3)
                {
                    issues = queryableIssue.OrderByDescending(e => e.IssueClosedDate).ToList();
                }
                else if (parameter.Sort == 4)
                {
                    issues = queryableIssue.OrderBy(e => e.IssueClosedDate).ToList();
                }
            }
            else
            {
                string sort = "";
                string query = "";

                var issueTypes = _context.IssueTypeList.Where(e => e.RaisedSystemId == parameter.RaisedSystem)
                    .ToList();
                if (issueTypes.Count > 0)
                {
                    query = " WHERE ";
                    var index = 0;
                    foreach (var it in issueTypes)
                    {
                        if (index == 0)
                        {
                            query += $"IssueTypeId={it.Id}";
                        }
                        else
                        {
                            query += $" or IssueTypeId={it.Id}";
                        }

                        index++;
                    }
                }

                if (parameter.Sort == 1)
                {
                    sort = $" Order By IssueRequestedDate desc";
                }
                else if (parameter.Sort == 2)
                {
                    sort = $" Order By IssueRequestedDate asc";
                }
                else if (parameter.Sort == 3)
                {
                    sort = $" Order By IssueClosedDate desc";
                }
                else if (parameter.Sort == 4)
                {
                    sort = $" Order By IssueClosedDate asc";
                }

                issues = queryableIssue.FromSql($"Select * From queryableIssue {query}{sort}").ToList();
            }

            foreach (var issue in issues)
            {
                var iss = GetIssueById(issue.Id);
                ret.Add(iss);
            }

            return ret;
        }

        public void AddIssueComment(IssueCommentsModel model)
        {
            var comment = new IssueComments()
            {
                Id = Guid.NewGuid(),
                IssueId = Guid.Parse(model.IssueId),
                IssueComment = model.IssueComment,
                CommentedBy = Guid.Parse(_session.UserId),
                CommentDate = DateTime.Now.Ticks
            };
            if (model.CommentResource.Count > 0)
            {
                var index = 0;
                var resourceModels = new List<ResourceModel>();
                foreach (var res in model.CommentResource)
                {
                    var resource = new ResourceModel()
                    {
                        DocRef = Guid.NewGuid().ToString(),
                        MimeType = res.MimeType,
                        FileName = res.FileName,
                        Data = "",
                        Index = index++
                    };
                    SaveResource(resource.DocRef, resource.MimeType, res.Data);
                    resourceModels.Add(resource);
                }

                comment.CommentResource = JsonConvert.SerializeObject(resourceModels);
            }


            _context.IssueComments.Add(comment);
            _context.SaveChanges();
            CreateActionTrackers(model.IssueId, "Commented on Issue", null, comment.Id.ToString());
            SetNotification(Guid.Parse(model.IssueId), "Commented on Issue", null, null, true);
        }

        public void EditIssueComment(IssueCommentsModel model)
        {
            var actionType = "Edited Comment on Issue";
            var existingComment = _context.IssueComments.FirstOrDefault(c => c.Id == Guid.Parse(model.Id));

            if (existingComment != null)
            {
                var actionDetails = JsonConvert.SerializeObject(existingComment);
                CreateActionTrackers(existingComment.IssueId.ToString(), actionType, actionDetails,
                    model.IssueComment);
                existingComment.IssueComment = model.IssueComment;
                if (model.CommentResource.Count > 0)
                {
                    var resourceModels = new List<ResourceModel>();
                    var index1 = 0;
                    foreach (var res in model.CommentResource)
                    {
                        index1++;
                        if (string.IsNullOrEmpty(res.DocRef))
                        {
                            var resource = new ResourceModel()
                            {
                                DocRef = Guid.NewGuid().ToString(),
                                MimeType = res.MimeType,
                                FileName = res.FileName,
                                Data = "",
                                Index = index1
                            };
                            SaveResource(resource.DocRef, resource.MimeType, res.Data);
                            resourceModels.Add(resource);
                        }
                        else
                        {
                            res.Data = "";
                            res.Index = index1;
                            resourceModels.Add(res);
                        }
                    }

                    existingComment.CommentResource = JsonConvert.SerializeObject(resourceModels);
                }


                _context.IssueComments.Update(existingComment);
                _context.SaveChanges();
                SetNotification(Guid.Parse(model.IssueId), actionType, null, null, true);
            }
            else
            {
                throw new Exception("Comment not found!");
            }
        }

        private void SaveResource(string fileName, string mimeType, string encodedStr)
        {
            var imgExt = mimeTypeRegx.Match(mimeType).Groups[2].Value;
            var path = Path.Combine($"{fileLocation}", $"{fileName}.{imgExt}");
            //path = path.Replace("\", "/" )
            if (File.Exists(path))
                return;
            var byteArr = Convert.FromBase64String(encodedStr);
            BinaryWriter binaryWriter = null;
            try
            {
                binaryWriter = new BinaryWriter(File.OpenWrite(path));

                binaryWriter.Write(byteArr);
                binaryWriter.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                binaryWriter?.Close();
            }
        }

        public string GetResourceDoc(string fileName, string mimeType)
        {
            var imageData = string.Empty;

            var imgExt = mimeTypeRegx.Match(mimeType).Groups[2].Value;
            var path = Path.Combine(fileLocation, $"{fileName}.{imgExt}");
            if (File.Exists(path))
            {
                var byteArr = File.ReadAllBytes(path);
                imageData = Convert.ToBase64String(byteArr);
            }

            return imageData;
        }

        // To retrieve all comments for a specific issue

        private IssueCommentReturnModel GetIssueCommentById(string commentId)
        {
            var comment = _context.IssueComments.FirstOrDefault(c => c.Id == Guid.Parse(commentId));
            var issueComments = new IssueCommentReturnModel();
            if (comment != null)
            {
                issueComments.Comment.Id = comment.Id.ToString();
                issueComments.Comment.IssueId = comment.IssueId.ToString();
                issueComments.Comment.CommentedBy = GetEmployee(comment.CommentedBy);
                issueComments.Comment.IssueComment = comment.IssueComment;
                issueComments.Comment.IssueCommentDate = new DateTime(comment.CommentDate ?? 0);
                if (!string.IsNullOrEmpty(comment.CommentResource))
                {
                    var imageResource = JsonConvert.DeserializeObject<IList<ResourceModel>>(comment.CommentResource);
                    foreach (var res in imageResource)
                    {
                        var data = GetResourceDoc(res.DocRef, res.MimeType);
                        if (!string.IsNullOrEmpty(data))
                        {
                            res.Data = data;
                            issueComments.Comment.CommentResource.Add(res);
                        }
                    }
                }

                var actionList = _context.ActionTracker.Where(e => e.Remark.Equals(commentId)).ToList();
                foreach (var action in actionList)
                {
                    issueComments.Actions.Add(GetActionTrackerById(action.Id));
                }
            }

            return issueComments;
        }

        private ActionTrackerReturnModel GetActionTrackerById(Guid id)
        {
            var actions = _context.ActionTracker.FirstOrDefault(e => e.Id == id);
            var ret = new ActionTrackerReturnModel();
            if (actions != null)
            {
                ret.Id = actions.Id.ToString();
                ret.IssueId = actions.IssueId.ToString();
                ret.UserId = GetEmployee(actions.UserId);
                ret.ActionType = actions.ActionType;
                ret.ActionDate = new DateTime(actions.ActionDate);
                ret.ActionDetails = actions.ActionDetails;
                ret.Remark = actions.Remark;
                ret.ActionTypeId = GetActionTypeId(actions.ActionType);
            }

            return ret;
        }

        public IList<IssueCommentReturnModel> GetAllIssueComments(string issueId)
        {
            var issueComments = new List<IssueCommentReturnModel>();
            var comments = _context.IssueComments.Where(e => e.IssueId == Guid.Parse(issueId)).ToList();
            foreach (var comm in comments)
            {
                issueComments.Add(GetIssueCommentById(comm.Id.ToString()));
            }

            return issueComments;
        }

        public void DeleteIssueComment(string commentId)
        {
            var comment = _context.IssueComments.Find(commentId);
            if (comment == null)
            {
                throw new Exception("Comment not found");
            }

            _context.IssueComments.Remove(comment);
            _context.SaveChanges();
        }

        public IList<DepartmentSchemaModel> GetAllBranch()
        {
            var ret = new List<DepartmentSchemaModel>();
            var branch = _context.DepartmentSchema.Where(d => d.Status == true).OrderBy(d => d.BranchId)
                .ThenBy(d => d.DepartmentId).ToList();
            foreach (var br in branch)
            {
                ret.Add(GetDepartment(br.Id));
            }

            return ret;
        }

        public IList<EmployeeModel> GetAllEmployee()
        {
            var ret = new List<EmployeeModel>();
            var employee = _context.Employee.Where(e => e.EmployeeStatus == 1).OrderBy(e => e.FirstName).ToList();
            foreach (var emp in employee)
            {
                ret.Add(GetEmployee(emp.Id));
            }

            return ret;
        }

        public IList<EmployeeModel> GetAllEmployeeByBranchId(string id)
        {
            var ret = new List<EmployeeModel>();
            var employee = _context.Employee.Where(e => e.EmployeeStatus == 1 && e.DepartmentId == Guid.Parse(id))
                .OrderBy(e => e.FirstName).ToList();
            foreach (var emp in employee)
            {
                ret.Add(GetEmployee(emp.Id));
            }

            return ret;
        }

        public void AssignIssue(AssignIssueModel model)
        {
            var existingAssignment = _context.IssueAssigned.Any(e =>
                e.IssueId == Guid.Parse(model.IssueId) && e.AssignedTo == Guid.Parse(model.AssignedTo));
            var actionType = "Assigned User to Issue";

            if (!existingAssignment)
            {
                var assignment = new IssueAssigned()
                {
                    Id = Guid.NewGuid(),
                    AssignedTo = Guid.Parse(model.AssignedTo),
                    AssignDate = DateTime.Now.Ticks,
                    IssueId = Guid.Parse(model.IssueId),
                    AssignedBy = Guid.Parse(_session.UserId)
                };

                _context.IssueAssigned.Add(assignment);
                _context.SaveChanges();

                var assignedIssue = _context.IssuesList.FirstOrDefault(c => c.Id == assignment.IssueId);
                if (assignedIssue != null)
                {
                    var username = _context.Account.First(e => e.EmployeeId == assignment.AssignedBy).Username;

                    var notif = new IssueNotification()
                    {
                        Id = Guid.NewGuid(),
                        NotificationTitle = assignedIssue.IssueTitle,
                        NotificationDetail = $"You have been assigned to Issue: {assignedIssue.IssueTitle} ",
                        NotificationFrom = assignedIssue.IssueRequestedBy,
                        NotificationTo = assignment.AssignedTo,
                        NotificationDate = new DateTime(assignment.AssignDate ?? 0).Ticks,
                        IssueId = assignedIssue.Id,
                        Status = false,
                    };

                    _context.IssueNotification.Add(notif);
                    _context.SaveChanges();

                    CreateActionTrackers(model.IssueId, actionType, null, model.AssignedTo);
                    SetNotification(Guid.Parse(model.IssueId), actionType, null, Guid.Parse(model.AssignedTo), true);
                }
            }
        }

        public void CloseIssue(string issueId, string remark)
        {
            var actionType = "Closed Issue";
            var issue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(issueId));
            if (issue != null)
            {
                if (issue.IssueStatus == 1)
                {
                    var hasDependencies = _context.IssueDependancies.Where(d => d.MajorIssue == Guid.Parse(issueId))
                        .ToList();
                    Boolean hasError = false;
                    string errorMessage = "";
                    foreach (var dep in hasDependencies)
                    {
                        if (_context.IssuesList.First(e => e.Id == dep.Dependancies).IssueStatus == 1)
                        {
                            hasError = true;
                            errorMessage = string.Format("{0}, ",
                                _context.IssuesList.First(e => e.Id == dep.Dependancies).Ticket);
                        }
                    }

                    if (!hasError)
                    {
                        issue.IssueClosedBy = Guid.Parse(_session.UserId);
                        issue.IssueClosedDate = DateTime.Now.Ticks;
                        issue.IssueStatus = 2;
                        _context.IssuesList.Update(issue);
                        _context.SaveChanges();
                        CloseDueDate(issue.Id);
                        CloseActiveTask(issue.Id);
                        CreateActionTrackers(issueId, actionType, null, remark);
                        SetNotification(Guid.Parse(issueId), actionType, null, null, true);
                    }
                    else
                    {
                        errorMessage = string.Format("Dependency issues of {0} are not closed", errorMessage);

                        throw new Exception(errorMessage);
                    }
                }
                else
                {
                    throw new Exception("Issue Already closed");
                }
            }
            else
            {
                throw new Exception("Issue Not Found");
            }
        }

        public void CancelIssue(string issueId, string remark)
        {
            var actionType = "Cancelled Issue";
            var issue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(issueId));
            if (issue != null)
            {
                if (issue.IssueStatus == 1 || issue.IssueStatus==3)
                {
                    var hasDependencies = _context.IssueDependancies.Where(d => d.MajorIssue == Guid.Parse(issueId))
                        .ToList();
                    Boolean hasError = false;
                    string errorMessage = "";
                    foreach (var dep in hasDependencies)
                    {
                        if (_context.IssuesList.First(e => e.Id == dep.Dependancies).IssueStatus == 1 || _context.IssuesList.First(e => e.Id == dep.Dependancies).IssueStatus == 3)
                        {
                            hasError = true;
                            errorMessage = string.Format("{0}, ",
                                _context.IssuesList.First(e => e.Id == dep.Dependancies).Ticket);
                        }
                    }

                    if (!hasError)
                    {
                        issue.IssueClosedBy = Guid.Parse(_session.UserId);
                        issue.IssueClosedDate = DateTime.Now.Ticks;
                        issue.IssueStatus = 4;
                        _context.IssuesList.Update(issue);
                        _context.SaveChanges();
                        CloseDueDate(issue.Id);
                        CloseActiveTask(issue.Id);
                        CreateActionTrackers(issueId, actionType, null, remark);
                        SetNotification(Guid.Parse(issueId), actionType, null, null, true);
                    }
                    else
                    {
                        errorMessage = string.Format("Dependency issues of {0} are not closed", errorMessage);

                        throw new Exception(errorMessage);
                    }
                }
                else
                {
                    throw new Exception("Issue Already closed/cancelled");
                }
            }
            else
            {
                throw new Exception("Issue Not Found");
            }
        }

        public void ReopenIssue(string issueId, string remark)
        {
            var actionType = "Re-Opened Issue";
            var issue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(issueId));
            if (issue != null)
            {
                if (issue.IssueStatus == 2)
                {
                    issue.IssueStatus = 1;
                    _context.IssuesList.Update(issue);
                    _context.SaveChanges();
                    CreateActionTrackers(issueId, actionType, null, remark);
                    SetNotification(Guid.Parse(issueId), actionType, issue.ForwardTo, null, true);
                }
                else
                {
                    throw new Exception("Issue Already Open");
                }
            }
            else
            {
                throw new Exception("Issue Not Found");
            }
        }

        public string PatchCloseIssue(PatchActionModel model)
        {
            string ret = "";
            int success = 0;
            int failed = 0;
            var actionType = "Closed Issue By Patch";
            foreach (var issues in model.CaseList)
            {
                var issue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(issues));
                if (issue != null)
                {
                    if (issue.IssueStatus == 1)
                    {
                        var hasDependencies = _context.IssueDependancies.Where(d => d.MajorIssue == Guid.Parse(issues))
                            .ToList();
                        Boolean hasError = false;
                        string errorMessage = "";
                        foreach (var dep in hasDependencies)
                        {
                            if (_context.IssuesList.First(e => e.Id == dep.Dependancies).IssueStatus == 1)
                            {
                                hasError = true;
                            }
                        }

                        if (!hasError)
                        {
                            issue.IssueClosedBy = Guid.Parse(_session.UserId);
                            issue.IssueClosedDate = DateTime.Now.Ticks;
                            issue.IssueStatus = 2;
                            _context.IssuesList.Update(issue);
                            _context.SaveChanges();
                            CloseDueDate(issue.Id);
                            CloseActiveTask(issue.Id);
                            CreateActionTrackers(issues, actionType, null, model.Remark);
                            SetNotification(issue.Id, actionType, issue.ForwardTo, null, true);
                            success++;
                        }
                        else
                        {
                            failed++;
                        }
                    }
                    else
                    {
                        failed++;
                    }
                }
                else
                {
                    failed++;
                }
            }

            ret = string.Format("Action Executed, with {1} success and {2} failed out off {0} total requested issues",
                model.CaseList.Count(), success, failed);
            return ret;
        }

        public string PatchReopenIssue(PatchActionModel model)
        {
            string ret = "";
            int success = 0;
            int failed = 0;
            var actionType = "Re-Opened Issue by Patch";
            foreach (var issues in model.CaseList)
            {
                var issue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(issues));
                if (issue != null)
                {
                    if (issue.IssueStatus == 2)
                    {
                        issue.IssueStatus = 1;
                        _context.IssuesList.Update(issue);
                        _context.SaveChanges();
                        CreateActionTrackers(issues, actionType, null, model.Remark);
                        SetNotification(issue.Id, actionType, issue.ForwardTo, null, true);
                        success++;
                    }
                    else
                    {
                        failed++;
                    }
                }
                else
                {
                    failed++;
                }
            }

            ret = string.Format("Action Executed, with {1} success and {2} failed out off {0} total requested issues",
                model.CaseList.Count(), success, failed);
            return ret;
        }
        public string PatchMakeReadNotification(PatchActionModel model)
        {
            string ret = "";
            int success = 0;
            int failed = 0;
            foreach (var notification in model.CaseList)
            {
                var notify = _context.IssueNotification.FirstOrDefault(i => i.Id == Guid.Parse(notification));
                if (notify != null)
                {
                    if (!notify.Status)
                    {
                        notify.Status = true;
                        _context.IssueNotification.Update(notify);
                        _context.SaveChanges();
                       
                        success++;
                    }
                    else
                    {
                        failed++;
                    }
                }
                else
                {
                    failed++;
                }
            }

            ret = string.Format("Action Executed, with {1} success and {2} failed out off {0} total requested issues",
                model.CaseList.Count(), success, failed);
            return ret;
        }

        public DashboardModel GetDashboard(string deptId)
        {
            var ret = new DashboardModel();

            if (!string.IsNullOrEmpty(deptId))
            {
                ret.Total = _context.IssuesList.Count(i => i.BranchId == Guid.Parse(deptId));
                ret.Closed = _context.IssuesList.Count(i => i.BranchId == Guid.Parse(deptId) && i.IssueStatus == 2);
                ret.Open = _context.IssuesList.Count(i => i.BranchId == Guid.Parse(deptId) && i.IssueStatus == 1);

                var systemRaised = _context.IssueRaisedSystem.ToList();
                var systems = new List<RaisedSystem>();
                foreach (var sy in systemRaised)
                {
                    var sys = new RaisedSystem();
                    sys.System.Id = sy.Id;
                    sys.System.Name = sy.Name;
                    sys.System.Total = 0;
                    sys.System.Closed = 0;
                    sys.System.Open = 0;
                    var systemIssueTypes = _context.IssueTypeList.Where(i => i.RaisedSystemId == sy.Id || i.Id == 0)
                        .OrderBy(e => e.Name)
                        .ToList();
                    foreach (var it in systemIssueTypes)
                    {
                        var issueType = new DashboardStat()
                        {
                            Id = it.Id,
                            Name = it.Name,
                            Total = _context.IssuesList.Count(i =>
                                (i.BranchId == Guid.Parse(deptId) && i.IssueTypeId == it.Id && i.IssueTypeId > 0) ||
                                (i.BranchId == Guid.Parse(deptId) && i.IssueTypeId == 0 &&
                                 i.OtherIssue == sy.Id.ToString())),
                            Closed = _context.IssuesList.Count(i =>
                                (i.BranchId == Guid.Parse(deptId) && i.IssueTypeId == it.Id && i.IssueStatus == 2 &&
                                 i.IssueTypeId > 0) ||
                                (i.BranchId == Guid.Parse(deptId) && i.IssueTypeId == 0 && i.IssueStatus == 2 &&
                                 i.OtherIssue == sy.Id.ToString())),
                            Open = _context.IssuesList.Count(i =>
                                (i.BranchId == Guid.Parse(deptId) && i.IssueTypeId == it.Id && i.IssueStatus == 1 &&
                                 i.IssueTypeId > 0) ||
                                (i.BranchId == Guid.Parse(deptId) && i.IssueTypeId == 0 && i.IssueStatus == 1 &&
                                 i.OtherIssue == sy.Id.ToString()))
                        };

                        sys.System.Total += issueType.Total;
                        sys.System.Closed += issueType.Closed;
                        sys.System.Open += issueType.Open;
                        sys.IssueType.Add(issueType);
                    }

                    ret.RaisedSystems.Add(sys);
                }

                var sql = string.Format(
                    "SELECT ac.* FROM issue_tracking.action_tracker ac inner join issue_tracking.issues_list il on il.id=ac.issue_id where il.branch_id='{0}' order by ac.action_date desc limit(15);",
                    deptId);
                var actions = _context.ActionTracker.FromSql(sql).ToList();
                foreach (var ac in actions)
                {
                    var iss = _context.IssuesList.First(a => a.Id == ac.IssueId);
                    var issl = new IssueSearchModel()
                    {
                        Id = iss.Id.ToString(),
                        TicketNo = iss.Ticket,
                        IssueTitle = iss.IssueTitle,
                        IssueType = _context.IssueTypeList.First(e => e.Id == iss.IssueTypeId).Name,
                        OtherIssue = iss.OtherIssue,
                        PolicyNo = iss.PolicyNo,
                        IssueDescription = iss.IssueDescription,
                        IssuePriority = _context.IssuePriorityType.First(e => e.Id == iss.IssuePriority).Name,
                        OpenedBy = GetEmployee(iss.IssueRequestedBy).Username,
                        OpeningDate = new DateTime(iss.IssueRequestedDate ?? 0),
                        Branch = GetDepartment(iss.BranchId).DepartmentName,
                        Status = iss.IssueStatus ?? 1,
                    };

                    var act = new ActionTrackerModel()
                    {
                        Id = ac.Id.ToString(),
                        IssueId = ac.IssueId.ToString(),
                        ActionDate = new DateTime(ac.ActionDate),
                        ActionType = ac.ActionType,
                        Issue = issl,
                        UserId = GetEmployee(ac.UserId),
                        Remark = ac.Remark,
                        ActionDetails = ac.ActionDetails,
                        ActionTypeId = GetActionTypeId(ac.ActionType)
                    };

                    if (ac.ActionType.Equals("Assigned User to Issue"))
                    {
                        act.Remark = string.Format("Assigned To {0}",
                            GetEmployee(Guid.Parse((ReadOnlySpan<char>)ac.Remark)).FirstName);
                    }

                    if (ac.ActionType.Equals("Removed Assigned User from Issue"))
                        act.Remark = string.Format("{0} Removed from Assign",
                            GetEmployee(Guid.Parse(ac.Remark)).FirstName);

                    if (ac.ActionType.Equals("Added Milestone to Issue"))
                        act.Remark = string.Format("Added Milestone is {0}",
                            GetMilestoneById(ac.Remark).Name);

                    if (ac.ActionType.Equals("Removed Milestone from issue"))
                        act.Remark = string.Format("Removed Milestone is {0}",
                            GetMilestoneById(ac.Remark).Name);

                    if (ac.ActionType.Equals("Added Dependencies for Issue"))
                        act.Remark = string.Format("Dependent Issue is {0}",
                            GetIssueById(Guid.Parse((ReadOnlySpan<char>)ac.Remark)).Ticket);

                    if (ac.ActionType.Equals("Removed Dependencies from Issue"))
                        act.Remark = string.Format("Dependent Issue is {0}",
                            GetIssueById(Guid.Parse((ReadOnlySpan<char>)ac.Remark)).Ticket);
                    if (ac.ActionType.Equals("Issue Forwarded To"))
                        act.Remark = string.Format("Forwarded to {0}",
                            GetForwardIssue(ac.Remark).ForwardToDept.DepartmentName);
                    ret.Actions.Add(act);
                }
            }
            else
            {
                ret.Total = _context.IssuesList.Count();
                ret.Closed = _context.IssuesList.Count(i => i.IssueStatus == 2);
                ret.Open = _context.IssuesList.Count(i => i.IssueStatus == 1);

                var systemRaised = _context.IssueRaisedSystem.ToList();
                var systems = new List<RaisedSystem>();
                foreach (var sy in systemRaised)
                {
                    var sys = new RaisedSystem();
                    sys.System.Id = sy.Id;
                    sys.System.Name = sy.Name;
                    sys.System.Total = 0;
                    sys.System.Closed = 0;
                    sys.System.Open = 0;
                    var systemIssueTypes = _context.IssueTypeList.Where(i => i.RaisedSystemId == sy.Id || i.Id == 0)
                        .OrderBy(i => i.Name)
                        .ToList();
                    foreach (var it in systemIssueTypes)
                    {
                        var issueType = new DashboardStat()
                        {
                            Id = it.Id,
                            Name = it.Name,
                            Total = _context.IssuesList.Count(i =>
                                (i.IssueTypeId == it.Id && i.IssueTypeId > 0) ||
                                (i.IssueTypeId == 0 && i.OtherIssue.Equals(sy.Id.ToString()))),
                            Closed = _context.IssuesList.Count(i =>
                                (i.IssueTypeId == it.Id && i.IssueStatus == 2 && i.IssueTypeId > 0) ||
                                (i.IssueTypeId == 0 && i.OtherIssue.Equals(sy.Id.ToString()) && i.IssueStatus == 2)),
                            Open = _context.IssuesList.Count(i =>
                                (i.IssueTypeId == it.Id && i.IssueStatus == 1 && i.IssueTypeId > 0) ||
                                (i.IssueTypeId == 0 && i.OtherIssue.Equals(sy.Id.ToString()) && i.IssueStatus == 1))
                        };

                        sys.System.Total += issueType.Total;
                        sys.System.Closed += issueType.Closed;
                        sys.System.Open += issueType.Open;
                        sys.IssueType.Add(issueType);
                    }

                    ret.RaisedSystems.Add(sys);
                }

                var sql =
                    "SELECT ac.* FROM issue_tracking.action_tracker ac inner join issue_tracking.issues_list il on il.id=ac.issue_id order by ac.action_date desc limit(15);";
                var actions = _context.ActionTracker.FromSql(sql).ToList();
                foreach (var ac in actions)
                {
                    var iss = _context.IssuesList.First(a => a.Id == ac.IssueId);
                    var issl = new IssueSearchModel()
                    {
                        Id = iss.Id.ToString(),
                        TicketNo = iss.Ticket,
                        IssueTitle = iss.IssueTitle,
                        IssueType = _context.IssueTypeList.First(e => e.Id == iss.IssueTypeId).Name,
                        OtherIssue = iss.OtherIssue,
                        PolicyNo = iss.PolicyNo,
                        IssueDescription = iss.IssueDescription,
                        IssuePriority = _context.IssuePriorityType.First(e => e.Id == iss.IssuePriority).Name,
                        OpenedBy = GetEmployee(iss.IssueRequestedBy).Username,
                        OpeningDate = new DateTime(iss.IssueRequestedDate ?? 0),
                        Branch = GetDepartment(iss.BranchId).DepartmentName,
                        Status = iss.IssueStatus ?? 1,
                    };

                    var act = new ActionTrackerModel()
                    {
                        Id = ac.Id.ToString(),
                        IssueId = ac.IssueId.ToString(),
                        ActionDate = new DateTime(ac.ActionDate),
                        ActionType = ac.ActionType,
                        Issue = issl,
                        UserId = GetEmployee(ac.UserId),
                        Remark = ac.Remark,
                        ActionDetails = ac.ActionDetails,
                        ActionTypeId = GetActionTypeId(ac.ActionType)
                    };

                    if (ac.ActionType.Equals("Assigned User to Issue"))
                    {
                        act.Remark = string.Format("Assigned To {0}",
                            GetEmployee(Guid.Parse((ReadOnlySpan<char>)ac.Remark)).FirstName);
                    }

                    if (ac.ActionType.Equals("Removed Assigned User from Issue"))
                        act.Remark = string.Format("{0} Removed from Assign",
                            GetEmployee(Guid.Parse(ac.Remark)).FirstName);

                    if (ac.ActionType.Equals("Added Milestone to Issue"))
                        act.Remark = string.Format("Added Milestone is {0}",
                            GetMilestoneById(ac.Remark).Name);

                    if (ac.ActionType.Equals("Removed Milestone from issue"))
                        act.Remark = string.Format("Removed Milestone is {0}",
                            GetMilestoneById(ac.Remark).Name);

                    if (ac.ActionType.Equals("Added Dependencies for Issue"))
                        act.Remark = string.Format("Dependent Issue is {0}",
                            GetIssueById(Guid.Parse((ReadOnlySpan<char>)ac.Remark)).Ticket);

                    if (ac.ActionType.Equals("Removed Dependencies from Issue"))
                        act.Remark = string.Format("Dependent Issue is {0}",
                            GetIssueById(Guid.Parse((ReadOnlySpan<char>)ac.Remark)).Ticket);

                    ret.Actions.Add(act);
                }
            }


            return ret;
        }

        public void EditMileStone(MileStonesModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                var mileStone = new Milestones()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    CreatedBy = Guid.Parse(_session.UserId),
                    CreatedDate = DateTime.Now.Ticks,
                    DueDate = model.DueDate.Ticks
                };
                _context.Milestones.Add(mileStone);
                _context.SaveChanges();
            }
            else
            {
                var oldMilestone = _context.Milestones.FirstOrDefault(e => e.Id == Guid.Parse(model.Id));
                if (oldMilestone != null)
                {
                    oldMilestone.Name = model.Name;
                    oldMilestone.Description = model.Description;
                    oldMilestone.DueDate = model.DueDate.Ticks;

                    _context.Milestones.Update(oldMilestone);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("I can't find any milestone to edit");
                }
            }
        }

        public IList<MileStonesModelReturn> GetAllMilestones()
        {
            var ret = new List<MileStonesModelReturn>();

            var milestones = _context.Milestones.OrderByDescending(m => m.CreatedDate).ToList();

            foreach (var ms in milestones)
            {
                ret.Add(GetMilestoneById(ms.Id.ToString()));
            }

            return ret;
        }

        public MileStonesModelReturn GetMilestoneById(string id)
        {
            var mileStone = _context.Milestones.FirstOrDefault(m => m.Id == Guid.Parse(id));
            var ret = new MileStonesModelReturn();
            if (mileStone != null)
            {
                ret.Id = mileStone.Id.ToString();
                ret.Name = mileStone.Name;
                ret.Description = mileStone.Description;
                ret.DueDate = new DateTime(mileStone.DueDate ?? 0);
                ret.CreatedBy = GetEmployee(mileStone.CreatedBy);
                ret.CreatedDate = new DateTime(mileStone.CreatedDate ?? 0);
            }

            return ret;
        }

        public void DeleteMilestone(string id)
        {
            var milestone = _context.Milestones.FirstOrDefault(m => m.Id == Guid.Parse(id));
            if (milestone != null)
            {
                var isIssueAva = _context.IssueMilestones.Any(e => e.MilestoneId == milestone.Id);
                if (!isIssueAva)
                {
                    _context.Milestones.Remove(milestone);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Sorry, there is an issue registered in milestones");
                }
            }
            else
            {
                throw new Exception("Sorry, i can't find any milestone for delete");
            }
        }

        public void AddMilestoneToIssue(string issueId, string milestoneId)
        {
            var isMileAva = _context.IssueMilestones.Any(e =>
                e.IssueId == Guid.Parse(issueId) && e.MilestoneId == Guid.Parse(milestoneId));
            if (!isMileAva)
            {
                var issueMileStone = new IssueMilestones()
                {
                    Id = Guid.NewGuid(),
                    IssueId = Guid.Parse(issueId),
                    MilestoneId = Guid.Parse(milestoneId)
                };
                _context.IssueMilestones.Add(issueMileStone);
                _context.SaveChanges();
                CreateActionTrackers(issueId, "Added Milestone to Issue", null, milestoneId);
                SetNotification(Guid.Parse(issueId), "Added Milestone to Issue", null, null, true);
            }
            else
            {
                throw new Exception("The milestone already added to this issue");
            }
        }

        public void RemoveMilestoneFromIssue(string id)
        {
            var issueMilestone = _context.IssueMilestones.FirstOrDefault(e => e.Id == Guid.Parse(id));
            if (issueMilestone != null)
            {
                CreateActionTrackers(issueMilestone.IssueId.ToString(), "Removed Milestone from issue", null,
                    issueMilestone.MilestoneId.ToString());
                _context.IssueMilestones.Remove(issueMilestone);
                _context.SaveChanges();
                SetNotification(issueMilestone.IssueId, "Removed Milestone from issue", null, null, true);
            }
            else
            {
                throw new Exception("sorry i can't find any milestone added to this issue");
            }
        }

        public void RemoveAssignFromIssue(string id)
        {
            var issueAssign = _context.IssueAssigned.FirstOrDefault(e => e.Id == Guid.Parse(id));
            if (issueAssign != null)
            {
                CreateActionTrackers(issueAssign.IssueId.ToString(), "Removed Assigned User from Issue", null,
                    issueAssign.AssignedTo.ToString());
                SetNotification(issueAssign.IssueId, "Removed Assigned User from Issue", null, null, true);
                _context.IssueAssigned.Remove(issueAssign);
                _context.SaveChanges();
            }
        }

        public IList<IssueSearchModel> GetAllDependents(string issueId)
        {
            var ret = new List<IssueSearchModel>();

            var dept = _context.IssuesList.First(e => e.Id == Guid.Parse(issueId)).BranchId;
            var activeIssue = _context.IssuesList
                .Where(e => e.Id != Guid.Parse(issueId) && e.IssueStatus == 1 && e.BranchId == dept).ToList();
            foreach (var ai in activeIssue)
            {
                var isAva = _context.IssueDependancies.Any(e =>
                    e.MajorIssue == Guid.Parse(issueId) && e.Dependancies == ai.Id);
                ret.Add(new IssueSearchModel()
                {
                    Id = ai.Id.ToString(),
                    TicketNo = ai.Ticket,
                    IssueTitle = ai.IssueTitle,
                    IssueDescription = ai.IssueDescription,
                    PolicyNo = ai.PolicyNo,
                    IssueType = _context.IssueTypeList.First(e => e.Id == ai.IssueTypeId).Name,
                    OtherIssue = ai.OtherIssue,
                });
            }

            return ret;
        }

        public void AddDependencyToIssue(string issueId, string depeId)
        {
            var isAva = _context.IssueDependancies.Any(d =>
                d.MajorIssue == Guid.Parse(issueId) && d.Dependancies == Guid.Parse(depeId));
            if (!isAva)
            {
                var dep = new IssueDependancies()
                {
                    Id = Guid.NewGuid(),
                    Dependancies = Guid.Parse(depeId),
                    MajorIssue = Guid.Parse(issueId)
                };

                _context.IssueDependancies.Add(dep);
                _context.SaveChanges();
                CreateActionTrackers(issueId, "Added Dependencies for Issue", null, depeId);
                SetNotification(Guid.Parse(issueId), "Added Dependencies for Issue", null, null, true);
            }
            else
            {
                throw new Exception("Sorry, the issue already registered as dependencies");
            }
        }

        public void RemoveDependencyFromIssue(string id)
        {
            var oldDependency = _context.IssueDependancies.FirstOrDefault(e => e.Id == Guid.Parse(id));
            if (oldDependency != null)
            {
                _context.IssueDependancies.Remove(oldDependency);
                _context.SaveChanges();
                CreateActionTrackers(oldDependency.MajorIssue.ToString(), "Removed Dependencies from Issue", null,
                    oldDependency.Dependancies.ToString());
                SetNotification(oldDependency.MajorIssue, "Removed Dependencies from Issue", null, null, true);
            }
            else
            {
                throw new Exception("Sorry, i can't find any dependencies to remove");
            }
        }

        public void StartTask(string issueId)
        {
            var isActiveTaskAva = _context.IssueTimeTracker.Any(t =>
                t.UserId == Guid.Parse(_session.UserId) && t.IssueId == Guid.Parse(issueId) &&
                t.Status.Equals("Started"));
            if (!isActiveTaskAva)
            {
                var timeTracker = new IssueTimeTracker()
                {
                    Id = Guid.NewGuid(),
                    IssueId = Guid.Parse(issueId),
                    UserId = Guid.Parse(_session.UserId),
                    StartTime = DateTime.Now.Ticks,
                    Status = "Started",
                };
                _context.IssueTimeTracker.Add(timeTracker);
                _context.SaveChanges();
                CreateActionTrackers(issueId, "Start Task on Issue", null, timeTracker.Id.ToString());
            }
            else
            {
                throw new Exception("Sorry, You already have active task");
            }
        }

        public void EndTask(string id)
        {
            var timeTracker = _context.IssueTimeTracker.FirstOrDefault(t => t.Id == Guid.Parse(id));
            if (timeTracker != null)
            {
                timeTracker.EndTime = DateTime.Now.Ticks;
                timeTracker.Status = "Ended";

                _context.IssueTimeTracker.Update(timeTracker);
                _context.SaveChanges();
                CreateActionTrackers(timeTracker.IssueId.ToString(), "End Task of Issue", null,
                    timeTracker.Id.ToString());
            }
            else
            {
                throw new Exception("Sorry, I can't find any started task with this id");
            }
        }

        public void AddDueDate(string issueId, DateTime dueDate)
        {
            var ava = _context.IssueDueDate.FirstOrDefault(i => i.IssueId == Guid.Parse(issueId));
            if (ava == null)
            {
                var dueDateModel = new IssueDueDate()
                {
                    Id = Guid.NewGuid(),
                    IssueId = Guid.Parse(issueId),
                    StartTime = DateTime.Now.Ticks,
                    DueDate = dueDate.Ticks,
                };
                if (DateTime.Now >= dueDate)
                {
                    dueDateModel.Status = "Inactive";
                }
                else
                {
                    dueDateModel.Status = "Active";
                }

                _context.IssueDueDate.Add(dueDateModel);
                _context.SaveChanges();
                CreateActionTrackers(issueId, "Set Due Date for Issue", null, dueDateModel.Id.ToString());
                SetNotification(Guid.Parse(issueId), "Set Due Date for Issue", null, null, true);
            }
            else
            {
                var oldDueDate = ava;
                ava.DueDate = dueDate.Ticks;
                if (DateTime.Now >= dueDate)
                {
                    ava.Status = "Inactive";
                }
                else
                {
                    ava.Status = "Active";
                }

                _context.IssueDueDate.Update(ava);
                _context.SaveChanges();
                CreateActionTrackers(issueId, "Update Due Date Of Issue", JsonConvert.SerializeObject(oldDueDate),
                    ava.Id.ToString());
                SetNotification(Guid.Parse(issueId), "Update Due Date Of Issue", null, null, true);
            }
        }

        public void DeleteIssueDueDate(string issueId)
        {
            var dueDate = _context.IssueDueDate.FirstOrDefault(i => i.IssueId == Guid.Parse(issueId));
            if (dueDate != null)
            {
                var oldDueDate = dueDate;
                _context.IssueDueDate.Remove(dueDate);
                _context.SaveChanges();
                CreateActionTrackers(issueId, "Remove Due Date of Issue", JsonConvert.SerializeObject(oldDueDate),
                    (new DateTime(oldDueDate.DueDate ?? 0)).ToString());
                SetNotification(Guid.Parse(issueId), "Remove Due Date of Issue", null, null, true);
            }
            else
            {
                throw new Exception("Sorry. I can't find any due date settled for this issue");
            }
        }


        private IssueListReturn GetSimpleIssueById(Guid id)
        {
            var ret = new IssueListReturn();
            var issue = _context.IssuesList.First(e => e.Id == id);


            return ret;
        }

        private DepartmentSchemaModel GetDepartment(Guid id)
        {
            var dept = _context.DepartmentSchema.First(e => e.Id == id);
            var ret = new DepartmentSchemaModel()
            {
                Id = dept.Id.ToString(),
                BranchId = dept.BranchId,
                DepartmentId = dept.DepartmentId,
                DepartmentName = _context.Department.First(d => d.Id == dept.DepartmentId).Name,
                BranchName = _context.Branches.First(b => b.Id == dept.BranchId).BraName
            };

            return ret;
        }

        private EmployeeModel GetEmployee(Guid id)
        {
            var employee = _context.Employee.First(e => e.Id == id);
            var ret = new EmployeeModel()
            {
                Id = employee.Id.ToString(),
                FirstName = employee.FirstName,
                FatherName = employee.FatherName,
                GrFatherName = employee.GrFatherName,
                EmpIdNo = employee.EmpIdNo,
                Username = _context.Account.First(e => e.EmployeeId == employee.Id).Username,
                PhoneNo = employee.Phone,
                Email = employee.Email
            };
            return ret;
        }

        private IList<SideBarStat> GetUpperSideBarStat()
        {
            var upperSidebars = new List<SideBarStat>();
            var departStat = new SideBarStat()
            {
                Id = 1,
                Name = "In Your Branch/ Dept",
                Stat = _context.IssuesList.Count(e => e.BranchId == Guid.Parse(_session.DepartmentId))
            };
            upperSidebars.Add(departStat);
            var createdByYou = new SideBarStat()
            {
                Id = 2,
                Name = "Created By You",
                Stat = _context.IssuesList.Count(e => e.IssueRequestedBy == Guid.Parse(_session.UserId))
            };
            upperSidebars.Add(createdByYou);
            var assignedToYou = new SideBarStat()
            {
                Id = 3,
                Name = "Assigned To You",
                Stat = _context.IssueAssigned.Count(e => e.AssignedBy == Guid.Parse(_session.UserId))
            };
            upperSidebars.Add(assignedToYou);
            return upperSidebars;
        }

        private IList<SideBarStat> GetLowerSideBarStat()
        {
            var lowerSidebars = new List<SideBarStat>();
            var systems = _context.IssueRaisedSystem.ToList();
            foreach (var system in systems)
            {
                int issueCount = 0;
                var issueTypes = _context.IssueTypeList.Where(e => e.RaisedSystemId == system.Id).ToList();
                foreach (var issueType in issueTypes)
                {
                    int counter = 0;
                    if (_session.Role == 1)
                    {
                        counter = _context.IssuesList.Count(e => e.IssueTypeId == issueType.Id);
                    }
                    else
                    {
                        counter = _context.IssuesList.Count(e =>
                            e.IssueTypeId == issueType.Id && e.IssueRequestedBy == Guid.Parse(_session.UserId));
                    }

                    issueCount += counter;
                }

                if (issueCount > 0)
                {
                    var sideBarStat = new SideBarStat()
                    {
                        Id = system.Id,
                        Name = system.Name,
                        Stat = issueCount
                    };
                    lowerSidebars.Add(sideBarStat);
                }
            }

            return lowerSidebars;
        }

        private void CreateActionTrackers(string issueId, string actionType, string actionDetails, string remark)
        {
            var actionTracker = new ActionTracker()
            {
                Id = Guid.NewGuid(),
                IssueId = Guid.Parse(issueId),
                UserId = Guid.Parse(_session.UserId),
                ActionDate = DateTime.Now.Ticks,
                ActionType = actionType,
                ActionDetails = actionDetails,
                Remark = remark
            };
            _context.ActionTracker.Add(actionTracker);
            _context.SaveChanges();
        }

        private IList<EmployeeModel> GetParticipant(Guid issueId)
        {
            var ret = new List<EmployeeModel>();

            var issueOwner = GetEmployee(_context.IssuesList.First(i => i.Id == issueId).IssueRequestedBy);
            ret.Add(issueOwner);
            var commenter = new List<Guid>();
            commenter.Add(Guid.Parse(issueOwner.Id));
            var par = _context.IssueComments.Where(i => i.IssueId == issueId).ToList();
            foreach (var p in par)
            {
                if (!commenter.Contains(p.CommentedBy))
                {
                    commenter.Add(p.CommentedBy);
                    ret.Add(GetEmployee(p.CommentedBy));
                }
            }

            var par2 = _context.ForwardTo.Where(i => i.IssueId == issueId).ToList();
            foreach (var p in par2)
            {
                if (!commenter.Contains(p.ForwardFrom))
                {
                    commenter.Add(p.ForwardFrom);
                    ret.Add(GetEmployee(p.ForwardFrom));
                }
            }

            return ret.OrderBy(p => p.Username).ToList();
        }

        public IList<EmployeeModel> GetPhoneBook(PhoneBookSearchParam model)
        {
            IList<EmployeeModel> ret = new List<EmployeeModel>();
            var employees = _context.Employee.Where(e =>
                    (string.IsNullOrEmpty(model.Name) || e.FirstName.ToLower().Contains(model.Name.ToLower()) ||
                     e.FatherName.ToLower().Contains(model.Name.ToLower()) ||
                     e.GrFatherName.ToLower().Contains(model.Name.ToLower()))
                    && (string.IsNullOrEmpty(model.EmpIdNo) || e.EmpIdNo.ToLower().Contains(model.EmpIdNo.ToLower()))
                    && (string.IsNullOrEmpty(model.DepartmentId) || e.DepartmentId == Guid.Parse(model.DepartmentId))
                    && (e.EmployeeStatus == 1)).OrderBy(e => e.FirstName)
                .ThenBy(e => e.FirstName).ToList();

            foreach (var emp in employees)
            {
                ret.Add(GetEmployee(emp.Id));
            }

            return ret;
        }

        public IList<DepartmentSchemaModel> GetHeadOfficeDept()
        {
            var ret = new List<DepartmentSchemaModel>();
            var dept = _context.DepartmentSchema.Where(e => e.BranchId == 10).ToList();
            foreach (var dp in dept)
            {
                ret.Add(GetDepartment(dp.Id));
            }

            return ret;
        }

        public void ForwardIssue(IssueForwardModel model)
        {
            var issue = _context.IssuesList.FirstOrDefault(e => e.Id == Guid.Parse(model.IssueId));
            if (issue != null)
            {
                if (issue.IssueStatus == 2)
                    throw new AccessDeniedException("Sorry, Issue already Closed");
                if (issue.IssueStatus == 4)
                    throw new AccessDeniedException("Sorry, Issue already Cancelled");
                if (issue.ForwardTo == Guid.Parse(model.ForwardToDept))
                    throw new AccessDeniedException("Sorry, Issue forward to the current location");

                var forwardModel = new ForwardTo()
                {
                    Id = Guid.NewGuid(),
                    IssueId = Guid.Parse(model.IssueId),
                    ForwardFrom = Guid.Parse(_session.UserId),
                    ForwardToDept = Guid.Parse(model.ForwardToDept),
                    Remark = model.Remark,
                    ForwardDate = DateTime.Now.Ticks,
                };
                if (!string.IsNullOrEmpty(model.ForwardToEmp))
                    forwardModel.ForwardToEmp = Guid.Parse(model.ForwardToEmp);

                if (model.IssueResource.Count > 0)
                {
                    var index = 0;
                    var resourceModels = new List<ResourceModel>();
                    foreach (var res in model.IssueResource)
                    {
                        var resource = new ResourceModel()
                        {
                            DocRef = Guid.NewGuid().ToString(),
                            FileName = res.FileName,
                            MimeType = res.MimeType,
                            Data = "",
                            Index = index++
                        };
                        SaveResource(resource.DocRef, resource.MimeType, res.Data);
                        resourceModels.Add(resource);
                    }

                    forwardModel.IssueResource = JsonConvert.SerializeObject(resourceModels);
                }

                issue.ForwardTo = Guid.Parse(model.ForwardToDept);
                if (model.ForwardToDept.Equals("f48cb514-8e36-4a87-a2e0-49042c096c99"))
                    issue.IssueStatus = 1;

                _context.IssuesList.Update(issue);
                _context.ForwardTo.Add(forwardModel);
                _context.SaveChanges();
                CreateActionTrackers(issue.Id.ToString(), "Issue Forwarded To", null, forwardModel.Id.ToString());
                if (forwardModel.ForwardToEmp != null)
                {
                    SetNotification(issue.Id, "Issue Forwarded To", null, forwardModel.ForwardToEmp, true);
                }
                else
                {
                    SetNotification(issue.Id, "Issue Forwarded To", forwardModel.ForwardToDept, null, true);
                }
            }
            else
            {
                throw new AccessDeniedException("Sorry, I can't find any issue registered with this id");
            }
        }

        private IList<AssignIssueReturnModel> GetAssigned(Guid issueId)
        {
            var emp = new List<AssignIssueReturnModel>();
            var assignment = _context.IssueAssigned.Where(e => e.IssueId == issueId).ToList();
            foreach (var ass in assignment)
            {
                emp.Add(new AssignIssueReturnModel()
                {
                    Id = ass.Id.ToString(),
                    IssueId = issueId.ToString(),
                    AssignedTo = GetEmployee(ass.AssignedTo),
                    AssignedBy = GetEmployee(ass.AssignedBy),
                    AssignDate = new DateTime(ass.AssignDate ?? 0)
                });
            }

            return emp;
        }

        private IList<LabelList> GetIssueLabelsList(Guid issueId)
        {
            var ret = new List<LabelList>();

            var labels = _context.Labels.Where(e => e.IssueId == issueId).ToList();
            foreach (var label in labels)
            {
                var lb = _context.LabelList.First(l => l.Id == label.LabelId);
                ret.Add(lb);
            }

            return ret;
        }

        private bool IsItStaffLoggedIn()
        {
            bool ret = false;
            var department = _context.DepartmentSchema.First(d => d.Id == Guid.Parse(_session.DepartmentId));
            if (department.BranchId == 10)
                ret = true;
            return ret;
        }

        private IList<IssueMilestonesReturn> GetissueMilestones(string issueId)
        {
            var ret = new List<IssueMilestonesReturn>();

            var milestones = _context.IssueMilestones.Where(e => e.IssueId == Guid.Parse(issueId))
                .OrderByDescending(e => e.Id).ToList();
            foreach (var ms in milestones)
            {
                var actions = _context.ActionTracker.First(e =>
                    e.IssueId == Guid.Parse(issueId) && e.Remark.Equals(ms.MilestoneId.ToString()));
                var mile = new IssueMilestonesReturn();
                mile.Id = ms.Id.ToString();
                mile.IssueId = ms.IssueId.ToString();
                mile.MilestoneId = ms.MilestoneId.ToString();
                mile.Name = GetMilestoneById(mile.MilestoneId).Name;
                mile.Description = GetMilestoneById(mile.MilestoneId).Description;
                mile.DueDate = GetMilestoneById(mile.MilestoneId).DueDate;
                mile.AddedBy = GetEmployee(actions.UserId);
                mile.AddedOn = new DateTime(actions.ActionDate);
                ret.Add(mile);
            }

            return ret;
        }

        private int GetActionTypeId(string name)
        {
            int ret = 0;
            if (name.Equals("Created Issue"))
                ret = 1;
            else if (name.Equals("Closed Issue"))
                ret = 2;
            else if (name.Equals("Re-Opened Issue"))
                ret = 3;
            else if (name.Equals("Re-Opened Issue by Patch"))
                ret = 4;
            else if (name.Equals("Closed Issue By Patch"))
                ret = 5;
            else if (name.Equals("Assigned User to Issue"))
                ret = 6;
            else if (name.Equals("Commented on Issue"))
                ret = 7;
            else if (name.Equals("Edited Comment on Issue"))
                ret = 8;
            else if (name.Equals("Edited Issue"))
                ret = 9;
            else if (name.Equals("Added Milestone to Issue"))
                ret = 10;
            else if (name.Equals("Removed Milestone from issue"))
                ret = 11;
            else if (name.Equals("Removed Assigned User from Issue"))
                ret = 12;
            else if (name.Equals("Added Dependencies for Issue"))
                ret = 13;
            else if (name.Equals("Removed Dependencies from Issue"))
                ret = 14;
            else if (name.Equals("Start Task on Issue"))
                ret = 15;
            else if (name.Equals("End Task of Issue"))
                ret = 16;
            else if (name.Equals("Set Due Date for Issue"))
                ret = 17;
            else if (name.Equals("Update Due Date Of Issue"))
                ret = 18;
            else if (name.Equals("Remove Due Date of Issue"))
                ret = 19;
            else if (name.Equals("Issue Forwarded To"))
                ret = 20;
            else if (name.Equals("Cancelled Issue"))
                ret = 21;

            return ret;
        }

        private LookupModel GetRaisedSystem(int id)
        {
            var ret = new LookupModel();
            var raSystem = _context.IssueRaisedSystem.FirstOrDefault(e => e.Id == id);
            if (raSystem != null)
            {
                ret.Id = raSystem.Id;
                ret.Name = raSystem.Name;
                ret.Description = raSystem.Description;
            }

            return ret;
        }

        private IList<IssueDependenciesReturn> GetAllIssueDependencies(string issueId)
        {
            var ret = new List<IssueDependenciesReturn>();

            var issues = _context.IssueDependancies.Where(e => e.MajorIssue == Guid.Parse(issueId)).ToList();
            foreach (var issue in issues)
            {
                ret.Add(GetIssueDependencies(issue.Id.ToString()));
            }

            return ret;
        }

        private IssueDependenciesReturn GetIssueDependencies(string id)
        {
            var issueDep = _context.IssueDependancies.First(e => e.Id == Guid.Parse(id));
            var issue = _context.IssuesList.First(e => e.Id == issueDep.Dependancies);
            var ret = new IssueDependenciesReturn()
            {
                Id = issueDep.Id.ToString(),
                DependentIssue = new IssueSearchModel()
                {
                    Id = issue.Id.ToString(),
                    TicketNo = issue.Ticket,
                    IssueTitle = issue.IssueTitle,
                    IssueDescription = issue.IssueDescription,
                    PolicyNo = issue.PolicyNo,
                    IssueType = _context.IssueTypeList.First(e => e.Id == issue.IssueTypeId).Name,
                    OtherIssue = issue.OtherIssue,
                }
            };
            var actions = _context.ActionTracker.First(e =>
                e.IssueId == issueDep.MajorIssue && e.Remark.Equals(issueDep.Dependancies.ToString()) &&
                e.ActionType.Equals("Added Dependencies for Issue"));
            ret.AddedBy = GetEmployee(actions.UserId);
            ret.AddedOn = new DateTime(actions.ActionDate);
            return ret;
        }

        private TimeTrackerReturn BuildTimeTracker(Guid issueId)
        {
            var ret = new TimeTrackerReturn()
            {
                MyActiveTask = null
            };
            var myActiveTask = _context.IssueTimeTracker.FirstOrDefault(t =>
                t.IssueId == issueId && t.UserId == Guid.Parse(_session.UserId) && t.Status.Equals("Started"));
            if (myActiveTask != null)
            {
                ret.MyActiveTask = GetTimeTrackerById(myActiveTask.Id);
            }

            var activeTask = _context.IssueTimeTracker.Where(t =>
                    t.IssueId == issueId && t.UserId != Guid.Parse(_session.UserId) && t.Status.Equals("Started"))
                .ToList();
            foreach (var act in activeTask)
            {
                ret.ActiveTask.Add(GetTimeTrackerById(act.Id));
            }

            var endedTask = _context.IssueTimeTracker.Where(t => t.IssueId == issueId && t.Status.Equals("Ended"))
                .ToList();
            foreach (var act in endedTask)
            {
                ret.EndedTask.Add(GetTimeTrackerById(act.Id));
            }

            return ret;
        }

        private TimeTrackerModel GetTimeTrackerById(Guid id)
        {
            var timeTracker = _context.IssueTimeTracker.First(t => t.Id == id);
            var ret = new TimeTrackerModel()
            {
                Id = timeTracker.Id.ToString(),
                IssueId = timeTracker.IssueId.ToString(),
                StartTime = new DateTime(timeTracker.StartTime),
                EndTime = new DateTime(timeTracker.EndTime ?? 0),
                Status = timeTracker.Status,
                Owner = GetEmployee(timeTracker.UserId)
            };
            return ret;
        }

        private void CloseDueDate(Guid issueId)
        {
            var dueDate = _context.IssueDueDate.FirstOrDefault(e => e.IssueId == issueId);
            if (dueDate != null)
            {
                dueDate.EndTime = DateTime.Now.Ticks;
                dueDate.Status = "Inactive";
                _context.IssueDueDate.Update(dueDate);
                _context.SaveChanges();
            }
        }

        private void CloseActiveTask(Guid issueId)
        {
            var taskList = _context.IssueTimeTracker.Where(e => e.IssueId == issueId && e.Status.Equals("Started"));
            var ret = new List<IssueTimeTracker>();
            foreach (var tl in taskList)
            {
                var task = tl;
                task.EndTime = DateTime.Now.Ticks;
                task.Status = "Ended";
                ret.Add(task);
            }

            _context.IssueTimeTracker.UpdateRange(ret);
            _context.SaveChanges();
        }

        private DueDateReturn GetDueDateReturn(Guid issueId)
        {
            var ret = new DueDateReturn();

            var dueDate = _context.IssueDueDate.FirstOrDefault(i => i.IssueId == issueId);
            if (dueDate != null)
            {
                var actions = _context.ActionTracker.First(e =>
                    e.IssueId == issueId && e.Remark.Equals(dueDate.Id.ToString()) &&
                    e.ActionType.Equals("Set Due Date for Issue"));

                ret.Id = dueDate.Id.ToString();
                ret.Status = dueDate.Status;
                ret.StartDate = new DateTime(dueDate.StartTime ?? 0);
                ret.EndDate = new DateTime(dueDate.EndTime ?? 0);
                ret.DueDate = new DateTime(dueDate.DueDate ?? 0);
                ret.SetBy = GetEmployee(actions.UserId);
            }
            else
            {
                ret = null;
            }


            return ret;
        }

        public IssueNotificationReturnModel GetNotification(Boolean status)
        {
            var ret = new IssueNotificationReturnModel();
            var notfi = new List<NotificationModel>();
            ret.UnreadNotification =
                _context.IssueNotification.Count(
                    n => n.NotificationTo == Guid.Parse(_session.UserId) && n.Status == false);
            ret.ReadNotification = _context.IssueNotification.Count(
                n => n.NotificationTo == Guid.Parse(_session.UserId) && n.Status == true);
            var notifications = _context.IssueNotification
                .Where(n => n.NotificationTo == Guid.Parse(_session.UserId) && n.Status == status)
                .OrderByDescending(n => n.NotificationDate).ToList();

            foreach (var nt in notifications)
            {
                var notification = new NotificationModel()
                {
                    Id = nt.Id.ToString(),
                    NotificationTitle = nt.NotificationTitle,
                    NotificationDetail = nt.NotificationDetail,
                    NotificationFrom = GetEmployee(Guid.Parse(nt.NotificationFrom.ToString())),
                    NotificationTo = nt.NotificationTo.ToString(),
                    IssueId = nt.IssueId.ToString(),
                    NotificationDate = new DateTime(nt.NotificationDate ?? 0),
                    Status = nt.Status,
                    TitleId = GetActionTypeId(nt.NotificationTitle)
                };
                notfi.Add(notification);
            }

            ret.Notifications = notfi;

            return ret;
        }

        public void MarkReadNotification(string notId)
        {
            var notification = _context.IssueNotification.First(n => n.Id == Guid.Parse(notId));
            notification.Status = true;
            _context.IssueNotification.Update(notification);
            _context.SaveChanges();
        }

        private string GetEmployeeId()
        {
            string employeeId = null;
            var users = _context.Account.FirstOrDefault(a => a.Username.Equals(_session.Username));
            if (users != null && users.EmployeeId != null)
            {
                employeeId = users.EmployeeId.ToString();
            }

            return employeeId;
        }

        private IssueForwardModelReturn GetForwardIssue(string id)
        {
            var ret = new IssueForwardModelReturn();
            var forward = _context.ForwardTo.First(e => e.Id == Guid.Parse(id));
            ret.Id = forward.Id.ToString();
            ret.IssueId = forward.IssueId.ToString();
            ret.ForwardDate = new DateTime(forward.ForwardDate);
            ret.Remark = forward.Remark;
            ret.ForwardFrom = GetEmployee(forward.ForwardFrom);
            ret.ForwardToDept = GetDepartment(forward.ForwardToDept);
            if (!string.IsNullOrEmpty(forward.IssueResource))
            {
                var imageResource = JsonConvert.DeserializeObject<IList<ResourceModel>>(forward.IssueResource);
                foreach (var res in imageResource)
                {
                    var data = GetResourceDoc(res.DocRef, res.MimeType);
                    if (!string.IsNullOrEmpty(data))
                    {
                        res.Data = data;
                        ret.IssueResource.Add(res);
                    }
                }
            }

            ret.ForwardToEmp = null;
            if (forward.ForwardToEmp != null)
                ret.ForwardToEmp = GetEmployee(forward.ForwardToEmp);
            return ret;
        }

        private IList<IssueForwardModelReturn> GetAllIssueForward(Guid id)
        {
            var ret = new List<IssueForwardModelReturn>();
            var forwards = _context.ForwardTo.Where(e => e.IssueId == id).OrderBy(e => e.ForwardDate).ToList();
            foreach (var issueFor in forwards)
            {
                ret.Add(GetForwardIssue(issueFor.Id.ToString()));
            }

            return ret;
        }

        private void SetNotification(Guid issueId, string title, Guid? deptId, Guid? empId, Boolean forParticipant)
        {
            var issue = _context.IssuesList.First(e => e.Id == issueId);
            var description = string.Format("#{0} : {1}", issue.Ticket, issue.IssueTitle);
            var notificationList = new List<IssueNotification>();
            var staffs = new List<Guid>();
            if (forParticipant)
            {
                var participant = GetParticipant(issueId);
                foreach (var pr in participant)
                {
                    if (!pr.Id.Equals(_session.UserId) && !staffs.Contains(Guid.Parse(pr.Id)))
                    {
                        var notif = new IssueNotification()
                        {
                            Id = Guid.NewGuid(),
                            NotificationDate = DateTime.Now.Ticks,
                            IssueId = issueId,
                            NotificationFrom = Guid.Parse(_session.UserId),
                            NotificationDetail = description,
                            Status = false,
                            NotificationTitle = title,
                            NotificationTo = Guid.Parse(pr.Id)
                        };
                        staffs.Add(Guid.Parse(pr.Id));
                        notificationList.Add(notif);
                    }
                }
            }

            if (empId != null)
            {
                if (!empId.ToString().Equals(_session.UserId) && !staffs.Contains(Guid.Parse(empId.ToString())))
                {
                    var notif = new IssueNotification()
                    {
                        Id = Guid.NewGuid(),
                        NotificationDate = DateTime.Now.Ticks,
                        IssueId = issueId,
                        NotificationFrom = Guid.Parse(_session.UserId),
                        NotificationDetail = description,
                        Status = false,
                        NotificationTitle = title,
                        NotificationTo = empId
                    };
                    staffs.Add(Guid.Parse(empId.ToString()));
                    notificationList.Add(notif);
                }
            }

            if (deptId != null)
            {
                var employees = GetAllEmployeeByBranchId(deptId.ToString());
                foreach (var pr in employees)
                {
                    if (!pr.Id.Equals(_session.UserId) && !staffs.Contains(Guid.Parse(pr.Id)))
                    {
                        var notif = new IssueNotification()
                        {
                            Id = Guid.NewGuid(),
                            NotificationDate = DateTime.Now.Ticks,
                            IssueId = issueId,
                            NotificationFrom = Guid.Parse(_session.UserId),
                            NotificationDetail = description,
                            Status = false,
                            NotificationTitle = title,
                            NotificationTo = Guid.Parse(pr.Id)
                        };
                        staffs.Add(Guid.Parse(pr.Id));
                        notificationList.Add(notif);
                    }
                }
            }

            _context.IssueNotification.AddRange(notificationList);
            _context.SaveChanges();
        }

        public int GetUnReadNotification()
        {
            int ret = _context.IssueNotification.Count(e =>
                e.NotificationTo == Guid.Parse(_session.UserId) && e.Status == false);
            return ret;
        }
    }
}