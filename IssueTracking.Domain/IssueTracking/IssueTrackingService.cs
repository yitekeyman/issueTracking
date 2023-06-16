using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
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
                // if (solution.Count>0)
                // {
                //     issueTypes.IssueSolution = solution;
                // }

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

                    if (model.SolutionResource != null)
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
                if (model.SolutionResource != null)
                {
                    var index = 0;
                    foreach (var res in model.SolutionResource)
                    {
                        var resource = new ResourceModel()
                        {
                            DocRef = Guid.NewGuid().ToString(),
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
                basicSolution.IssueType = _context.IssueTypeList.First(i => i.Id == solution.IssueTypeId);
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

        private string GetResourceDoc(string fileName, string mimeType)
        {
            var imageData = string.Empty;

            var imgExt = mimeTypeRegx.Match(mimeType).Groups[2].Value;
            var path = Path.Combine(fileLocation, $"{fileName}.{imgExt}");
            var byteArr = File.ReadAllBytes(path);
            imageData = Convert.ToBase64String(byteArr);


            return imageData;
        }
    }
}