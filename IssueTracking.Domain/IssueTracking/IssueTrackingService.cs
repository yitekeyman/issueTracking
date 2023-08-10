using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IssueTracking.Domain.IssueTracking
{
    public interface IIssueTrackingService
    {
        void SetSession(UserSession session);
        UserSession Login(LoginModel model);
        IssuePriorityType GetPriorityTypeById(long id);
        IList<IssuePriorityType> GetAllPriorityTypes();
        IList<IssueStatusType> GetAllIssueStatusTypes();
        IssueRaisedSystem GetIssueRaisedSystemById(long id);
        IList<IssueRaisedSystem> GetAllIssueRaisedSystems();
        IssueRaisedSystemReturn GetRaisedSystemById(long id);
        IList<IssueRaisedSystemReturn> GetAllRaisedSystems();
        void EditIssueType(IssueTypeList model);
        IssueTypeReturn GetIssueTypeById(long id);
        IList<IssueTypeReturn> GetAllIssueType();

        void EditBasicIssueSolution(BasicSolutionModel model);
        BasicSolutionReturn GetBasicSolutionById(long id);
        IList<BasicSolutionReturn> GetBasicSolutionByIssueType(long id);
        IList<BasicSolutionReturn> GetAllBasicSolution();
        string GetResourceDoc(string fileName, string mimeType);
        void AddIssue(IssuesListModel model);
        void EditIssue(IssuesListModel model);
        IList<IssueListReturn> GetAllIssues();
        IList<IssueListReturn> GetIssueByStatus(IssueFilterParameter parameter, long status);
        IssueListReturn GetIssueById(Guid id);

        void AddIssueComment(IssueCommentsModel model);
        void EditIssueComment(IssueCommentsModel model);
        IList<IssueCommentsModel> GetAllIssueComments(string issueId);
        void DeleteIssueComment(string commentId);
        
        IList<DepartmentSchemaModel> GetAllBranch();
        IList<EmployeeModel> GetAllEmployee();
        IList<EmployeeModel> GetAllEmployeeByBranchId(string id);
        
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

        public IssueRaisedSystem GetIssueRaisedSystemById(long id)
        {
            return _context.IssueRaisedSystem.First(i => i.Id == id);
        }

        public IList<IssueRaisedSystem> GetAllIssueRaisedSystems()
        {
            return _context.IssueRaisedSystem.OrderBy(e => e.Id).ToList();
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
            var raisedSystem = _context.IssueRaisedSystem.OrderBy(e => e.Id).ToList();
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
                if (solution.Count>0)
                {
                    foreach (var sol in solution)
                    {
                        var sl = new BasicIssueSolution()
                        {
                            Id = sol.Id,
                            SolutionDescription = sol.SolutionDescription,
                            SolutionQuery = sol.SolutionQuery
                        };
                        issueTypes.IssueSolution.Add(sl);
                    }
                   
                   
                }

                if (raisedSystem != null)
                {
                    issueTypes.RaisedSystem = raisedSystem;
                }
            }

            return issueTypes;
        }

        public IList<IssueTypeReturn> GetAllIssueType()
        {
            var issueTypeList = new List<IssueTypeReturn>();
            var issues = _context.IssueTypeList.OrderBy(e => e.Id).ToList();
            foreach (var issue in issues)
            {
                var issueType = GetIssueTypeById(issue.Id);
                if (issueType != null && issueType.Id > 0)
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

                    if (model.SolutionResource.Count>0)
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
                if (model.SolutionResource.Count>0)
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
        
        public IssueListReturn GetIssueById(Guid id) {
        
           var issueList = new IssueListReturn();
           
           var issue = _context.IssuesList.FirstOrDefault(e => e.Id == id);
           if (issue != null)
           {
                issueList.Id = issue.Id.ToString();
                issueList.IssueTitle = issue.IssueTitle;
                issueList.IssueTypeId = issue.IssueTypeId;
                issueList.OtherIssue = issue.OtherIssue;
                issueList.PolicyNo = issue.PolicyNo;
                issueList.BranchId = GetDepartment(issue.BranchId);
                issueList.IssueDescription = issue.IssueDescription;
                issueList.IssueRequestedBy =  GetEmployee(issue.IssueRequestedBy);
                issueList.IssueRequestedDate = DateTime.Now;
                issueList.IssuePriority = _context.IssuePriorityType.First(e=>e.Id==issue.IssuePriority).Name;
                issueList.IssueStatus = _context.IssueStatusType.First(e=>e.Id==issue.IssueStatus).Name;
                issueList.Ticket = issue.Ticket;
                issueList.Participant = 1+_context.IssueAssigned.Count(e=>e.IssueId==issue.Id);
                issueList.Comments = _context.IssueComments.Count(e=>e.IssueId==issue.Id);
                issueList.NoOfEdit =  _context.DeletedIssuesList.Count(e=>e.OldIssueId==issue.Id);
                issueList.IssueType = GetIssueTypeById(issue.IssueTypeId??0);
                 if (!string.IsNullOrEmpty(issue.IssueResource))
                 {
                       var imageResource = JsonConvert.DeserializeObject<IList<ResourceModel>>(issue.IssueResource);
                       foreach (var res in imageResource) {
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

        private void SaveResource(string fileName, string mimeType, string encodedStr)
        {
            var imgExt = mimeTypeRegx.Match(mimeType).Groups[2].Value;
            var path = Path.Combine(fileLocation, $"{fileName}.{imgExt}");
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
            var byteArr = File.ReadAllBytes(path);
            imageData = Convert.ToBase64String(byteArr);


            return imageData;
        }

        public void AddIssue(IssuesListModel model)
        {
            var count = (_context.IssuesList.Count()+1).ToString();
            var pt = "000000";
            var ticket = pt.Substring(0, pt.Length - count.Length) + count;
            var solutionId = _context.BasicIssueSolution.FirstOrDefault(e => e.IssueTypeId == model.IssueTypeId);
            var issue = new IssuesList()
            {
                Id = Guid.NewGuid(),
                IssueTitle = model.IssueTitle,
                IssueTypeId = model.IssueTypeId,
                OtherIssue = model.OtherIssue,
                BranchId = Guid.Parse((ReadOnlySpan<char>)_session.DepartmentId),
                IssueDescription = model.IssueDescription,
                IssueRequestedBy = Guid.Parse(_session.UserId),
                IssueRequestedDate = new DateTime().Ticks,
                IssuePriority = model.IssuePriority,
                IssueStatus = 1,
                Ticket = ticket
            };
            if (solutionId != null)
            {
                issue.IssueRaisedSluId = solutionId.Id;
            }
            if (model.PolicyNo.Length > 0)
            {
                issue.PolicyNo = model.PolicyNo;
            }
            if (model.IssueResource.Count>0)
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
        }

        public void EditIssue(IssuesListModel model)
        {
            var index = _context.DeletedIssuesList.Count(e => e.OldIssueId == Guid.Parse(model.Id)) + 1;
            var oldIssue = _context.IssuesList.FirstOrDefault(i => i.Id == Guid.Parse(model.Id));
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

                oldIssue.IssueTypeId = model.IssueTypeId;
                oldIssue.OtherIssue = model.OtherIssue;
                oldIssue.IssueTitle = model.IssueTitle;
                oldIssue.IssueDescription = model.IssueDescription;
                oldIssue.IssuePriority = model.IssuePriority;

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
            }
            else
            {
                throw new AccessDeniedException("Sorry, I can't find any issue registered with this title of '"+model.IssueTitle+"'");
            }
        }

        public IList<IssueListReturn> GetAllIssues()
        {
            var allIssues = new List<IssueListReturn>();
            var issues = _context.IssuesList.OrderBy(i => i.Id).ToList();
            foreach (var issue in issues)
            {
                var ai = GetIssueById(issue.Id);
                if (ai != null &&  int.TryParse(ai.Id, out int id) && id > 0)
                {
                    allIssues.Add(ai);
                }
            }

            return allIssues;
        }
        
        public IssueListReturnModel GetAllIssues(IssueFilterParameter parameter)
        {
            var opened = new List<IssueListReturn>();
            opened.AddRange(GetIssueByStatus(parameter,1));
            opened.AddRange(GetIssueByStatus(parameter,3));
            IssueListReturnModel ret = new IssueListReturnModel()
            {
                Opened= opened,
                Closed = GetIssueByStatus(parameter,2)
            };
            return ret;
        }

        public IList<IssueListReturn> GetIssueByStatus(IssueFilterParameter parameter,long status)
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
                    }else if (parameter.Sort == 2)
                    {
                        issues = queryableIssue.OrderBy(e => e.IssueRequestedDate).ToList();
                    }else if (parameter.Sort == 3)
                    {
                         issues = queryableIssue.OrderByDescending(e => e.IssueClosedDate).ToList();
                    }else if (parameter.Sort == 4)
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
                    }else if (parameter.Sort == 2)
                    {
                        sort = $" Order By IssueRequestedDate asc";
                    }else if (parameter.Sort == 3)
                    {
                        sort = $" Order By IssueClosedDate desc";
                    }else if (parameter.Sort == 4)
                    {
                        sort = $" Order By IssueClosedDate asc";
                    }

                    issues = queryableIssue.FromSql($"Select * From queryableIssue {query}{sort}").ToList();
                }
               
            foreach (var issue in issues)
            {
                var iss = new IssueListReturn()
                {
                    Id = issue.Id.ToString(),
                    IssueTitle = issue.IssueTitle,
                    IssueTypeId = issue.IssueTypeId,
                    OtherIssue = issue.OtherIssue,
                    PolicyNo = issue.PolicyNo,
                    IssueDescription = issue.IssueDescription,
                    BranchId = GetDepartment(issue.BranchId),
                    IssueRequestedBy = GetEmployee(issue.IssueRequestedBy),
                    IssuePriority = _context.IssuePriorityType.First(e=>e.Id==issue.IssuePriority).Name,
                    IssueStatus = _context.IssueStatusType.First(e=>e.Id==issue.IssueStatus).Name,
                    IssueType = GetIssueTypeById(issue.IssueTypeId??0),
                    Ticket = issue.Ticket,
                    Participant = 1+_context.IssueAssigned.Count(e=>e.IssueId==issue.Id),
                    Comments = _context.IssueComments.Count(e=>e.IssueId==issue.Id),
                    NoOfEdit = _context.DeletedIssuesList.Count(e=>e.OldIssueId==issue.Id),
                };
               ret.Add(iss);
            }
            return ret;
        }

        public void AddIssueComment(IssueCommentsModel model)
        {
            var comment = new IssueComments()
            {
               IssueId = Guid.Parse(model.IssueId),
               IssueComment = model.IssueComment,
               CommentedBy = Guid.Parse(_session.UserId),
               CommentDate = model.IssueCommentDate.Date
            };
            if (model.CommentResource.Count > 0)
            {
                var resourceModels = new List<ResourceModel>();
                foreach (var res in model.CommentResource)
                {
                    var resource = new ResourceModel()
                    {
                       DocRef = Guid.NewGuid().ToString(),
                       MimeType = res.MimeType,
                       FileName = res.FileName,
                       Data = "",
                       Index = res.Index
                    };
                    SaveResource(resource.DocRef, resource.MimeType,res.Data);
                    resourceModels.Add(resource);
                }
                comment.CommentResource = JsonConvert.SerializeObject(resourceModels);
            }

            if (model.IssueStatus != null)
            {
                comment.IssueStatus = model.IssueStatus.Id;
            }

            _context.IssueComments.Add(comment);
            _context.SaveChanges();
        }

        public void EditIssueComment(IssueCommentsModel model)
        {
            var existingComment = _context.IssueComments.FirstOrDefault(c => c.Id == Guid.Parse(model.Id));
            if (existingComment != null)
            {
                existingComment.IssueComment = model.IssueComment;
                existingComment.ModifiedDate = model.ModifiedDate.Date;
                if (model.CommentResource.Count > 0)
                {
                    var resourceModels = new List<ResourceModel>();
                    // Update resources 
                    existingComment.CommentResource = JsonConvert.SerializeObject(resourceModels);
                }
                
                if (model.IssueStatus != null)
                {
                    existingComment.IssueStatus = model.IssueStatus.Id;
                }
                
                _context.IssueComments.Update(existingComment);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Comment not found!");
            }
        }
        // To retrieve all comments for a specific issue
        public IList<IssueCommentsModel> GetAllIssueComments(string issueId)  
        {
            var issueGuid = new Guid(issueId);
        
            return _context.IssueComments
                .Where(c => c.IssueId == issueGuid)
                .Select(c => new IssueCommentsModel() 
                {
                    Id = c.Id.ToString(),        
                    IssueId = c.IssueId.ToString(), 
                    IssueComment = c.IssueComment,
                    CommentedBy = c.CommentedBy.ToString(),
                    IssueCommentDate = c.CommentDate,           
                    CommentResource = JsonConvert.DeserializeObject<List<ResourceModel>>(c.CommentResource),
                    IssueStatus = new IssueStatusType() 
                    {
                        Id = (long) c.IssueStatus,
                        Name = _context.IssueStatusType
                            .Where(s => s.Id == c.IssueStatus)
                            .Select(s => s.Name)
                            .FirstOrDefault(),                 
                        Description = _context.IssueStatusType
                            .Where(s => s.Id == c.IssueStatus)  
                            .Select(s => s.Description)
                            .FirstOrDefault()
                    },               
                    Status = 1           
                })
                .ToList();      
        }
        
        public void DeleteIssueComment(string commentId) {

            var comment = _context.IssueComments.Find(commentId);
            if(comment == null) {
                throw new Exception("Comment not found");   
            }

            _context.IssueComments.Remove(comment);
            _context.SaveChanges();

        }
        

        public IList<DepartmentSchemaModel> GetAllBranch()
        {
            var ret = new List<DepartmentSchemaModel>();
            var branch = _context.DepartmentSchema.Where(d => d.Status == true).ToList();
            foreach (var br in branch)
            {
                ret.Add(GetDepartment(br.Id));
            }
            return ret;
        }

        public IList<EmployeeModel> GetAllEmployee()
        {
            var ret = new List<EmployeeModel>();
            var employee = _context.Employee.Where(e => e.EmployeeStatus == 1).OrderBy(e=>e.FirstName).ToList();
            foreach (var emp in employee)
            {
                ret.Add(GetEmployee(emp.Id));
            }

            return ret;
        }

        public IList<EmployeeModel> GetAllEmployeeByBranchId(string id)
        {
            var ret = new List<EmployeeModel>();
            var employee = _context.Employee.Where(e => e.EmployeeStatus == 1 && e.DepartmentId==Guid.Parse(id)).OrderBy(e=>e.FirstName).ToList();
            foreach (var emp in employee)
            {
                ret.Add(GetEmployee(emp.Id));
            }

            return ret; 
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
                DepartmentName = _context.Department.First(d=>d.Id==dept.DepartmentId).Name,
                BranchName = _context.Branches.First(b=>b.Id==dept.BranchId).BraName
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
                Appellation = employee.Applelation,
                Title = employee.Title,
                Position = _context.Possitions.First(p=>p.Id==employee.PossitionId).Name,
                EmpIdNo = employee.EmpIdNo,
                Phone = employee.Phone,
                Email = employee.Email,
                Username = _context.Account.First(e=>e.EmployeeId==employee.Id).Username
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
                var issueTypes = _context.IssueTypeList.Where(e=>e.RaisedSystemId==system.Id).ToList();
                foreach (var issueType in issueTypes)
                {
                    int counter = 0;
                    if (_session.Role == 1)
                    {
                        counter = _context.IssuesList.Count(e => e.IssueTypeId == issueType.Id);
                    }
                    else
                    {
                        counter = _context.IssuesList.Count(e => e.IssueTypeId == issueType.Id && e.IssueRequestedBy==Guid.Parse(_session.UserId));
                    }
                    issueCount += counter;
                }

                if (issueCount > 0)
                {
                    var sideBarStat=new SideBarStat()
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
    }
}