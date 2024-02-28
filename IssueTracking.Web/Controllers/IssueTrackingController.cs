using System;
using System.Collections.Generic;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using IssueTracking.Domain.IssueTracking;
using IssueTracking.Web.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracking.Web.Controllers
{
    public class IssueTrackingController : BaseController
    {
        static Dictionary<String, UserSession> sessions = new Dictionary<string, UserSession>();

        public static UserSession GetSession(string sid)
        {
            if (sessions.ContainsKey(sid))
                return sessions[sid];
            return null;
        }

        private readonly IIssueTrackingFacade _iIssueTrackingFacade;

        public IssueTrackingController(IIssueTrackingFacade iIssueTrackingFacade)
        {
            _iIssueTrackingFacade = iIssueTrackingFacade;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                var user = _iIssueTrackingFacade.Login(null, model);

                var us = new UserSession()
                {
                    Username = user.Username,
                    CreatedTime = DateTime.Now,
                    LastSeen = DateTime.Now,
                    UserId = user.UserId,
                    DepartmentId = user.DepartmentId,
                    Role = user.Role,
                    Id = Guid.NewGuid().ToString()
                };
                HttpContext.Session.SetSession("sessionInfo", us);
                var sid = base.HttpContext.Session.Id;
                lock (sessions)
                {
                    if (sid != null)
                    {
                        if (sessions.ContainsKey(sid))
                            sessions.Remove(sid);
                        sessions.Add(sid, us);
                    }
                }

                return Json(new
                    { role = us.Role, username = us.Username, userId = us.UserId, departmentId = us.DepartmentId });
            }
            catch (Exception e)
            {
                return StatusCode(401, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetPriorityTypeById([FromQuery] long id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetPriorityTypeById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllPriorityTypes()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllPriorityTypes(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetIssueRaisedSystemById([FromQuery] long id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetIssueRaisedSystemById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllIssueRaisedSystems()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllIssueRaisedSystems(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetRaisedSystemById([FromQuery] long id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetRaisedSystemById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllRaisedSystems()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllRaisedSystems(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult EditIssueType([FromBody] IssueTypeList model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditIssueType(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetIssueTypeById([FromQuery] long id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetIssueTypeById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetIssueById([FromQuery] string id)
        {
            try
            {
                var ret = _iIssueTrackingFacade.GetIssueById(GetSession(), Guid.Parse(id));
                return Json(ret);
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        /*
        [HttpGet]
        public IActionResult GetsIssueById([FromQuery] Guid id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetsIssueById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
         */
        [HttpPost]
        public IActionResult EditBasicIssueSolution([FromBody] BasicSolutionModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditBasicIssueSolution(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllIssueType()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllIssueType(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetBasicSolutionById([FromQuery] long id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetBasicSolutionById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetBasicSolutionByIssueType([FromQuery] long id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetBasicSolutionByIssueType(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllBasicSolution()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllBasicSolution(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetResourceDoc([FromQuery] string fileName, string mimeType, string docRef)
        {
            try
            {
                var image = _iIssueTrackingFacade.GetResourceDoc(GetSession(), docRef, mimeType);
                var data = Convert.FromBase64String(image);
                return File(data, mimeType, fileName);
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllIssueStatusTypes()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllIssueStatusTypes(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult AddIssue([FromBody] IssuesListModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                return Json(_iIssueTrackingFacade.AddIssue(GetSession(), model));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult EditIssue([FromBody] IssuesListModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditIssue(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult GetAllIssues([FromBody] QueryParams model)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllIssues(GetSession(), model));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        // [HttpGet]
        // public IActionResult GetIssueByStatus([FromQuery] long status)
        // {
        //     try
        //     {
        //         return Json(_iIssueTrackingFacade.GetIssueByStatus(GetSession(), status));
        //         
        //     }
        //     catch (Exception e)
        //     {
        //         var stCode = 500;
        //         if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
        //             stCode = 400;
        //         return StatusCode(stCode, new { message = e.Message });
        //     }
        // }
        //
        [HttpGet]
        public IActionResult GetAllBranch()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllBranch(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllEmployee(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllEmployeeByBranchId([FromQuery] string id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllEmployeeByBranchId(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Logout(String sid)
        {
            if (!string.IsNullOrEmpty(sid))
            {
                lock (sessions)
                {
                    if (sessions.ContainsKey(sid))
                        sessions.Remove(sid);
                }
            }

            base.HttpContext.Session.Clear();
            return Json(true);
        }

        [HttpGet]
        public IActionResult GetFilePath(string fileName, string mimeType)
        {
            try
            {
                var doc = _iIssueTrackingFacade.GetResourceDoc(GetSession(), fileName, mimeType);
                var byteArr = Convert.FromBase64String(doc);
                return File(byteArr, mimeType, null);
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult AssignIssue([FromBody] AssignIssueModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.AssignIssue(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult AddComment([FromBody] IssueCommentsModel model)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.AddIssueComment(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult EditComment([FromBody] IssueCommentsModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditIssueComment(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult CloseIssue([FromQuery] string issueId, string remark)
        {
            try
            {
                _iIssueTrackingFacade.CloseIssue(GetSession(), issueId, remark);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult ReopenIssue([FromQuery] string issueId, string remark)
        {
            try
            {
                _iIssueTrackingFacade.ReopenIssue(GetSession(), issueId, remark);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult PatchCloseIssue([FromBody] PatchActionModel model)
        {
            try
            {
                return Json(_iIssueTrackingFacade.PatchCloseIssue(GetSession(), model));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult PatchReopenIssue([FromBody] PatchActionModel model)
        {
            try
            {
                return Json(_iIssueTrackingFacade.PatchReopenIssue(GetSession(), model));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpGet]
        public IActionResult GetDashboard([FromQuery] string model)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetDashboard(GetSession(), model));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpPost]
        public IActionResult EditMileStone([FromBody] MileStonesModel model)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditMileStone(GetSession(), model);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpGet]
        public IActionResult GetAllMilestones()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllMilestones(GetSession()));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpGet]
        public IActionResult GetMilestoneById([FromQuery] string id)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetMilestoneById(GetSession(), id));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpPost]
        public IActionResult DeleteMilestone([FromQuery] string id)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.DeleteMilestone(GetSession(), id);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpPost]
        public IActionResult AddMilestoneToIssue([FromQuery] string issueId, string milestoneId)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.AddMilestoneToIssue(GetSession(), issueId, milestoneId);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult RemoveMilestoneFromIssue([FromQuery] string id)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.RemoveMilestoneFromIssue(GetSession(), id);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpPost]
        public IActionResult RemoveAssignFromIssue([FromQuery] string id)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.RemoveAssignFromIssue(GetSession(), id);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpGet]
        public IActionResult GetAllDependents([FromQuery] string issueId)
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetAllDependents(GetSession(), issueId));
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult AddDependencyToIssue([FromQuery] string issueId, string depeId)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.AddDependencyToIssue(GetSession(), issueId, depeId);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult RemoveDependencyFromIssue([FromQuery] string id)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.RemoveDependencyFromIssue(GetSession(), id);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult StartTask([FromQuery] string issueId)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.StartTask(GetSession(), issueId);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult EndTask([FromQuery] string id)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EndTask(GetSession(), id);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
        [HttpPost]
        public IActionResult AddDueDate([FromQuery] string issueId, DateTime dueDate)
        {
            // if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.AddDueDate(GetSession(), issueId, dueDate);
                return StatusCode(200, new { message = true });
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.Message });
            }
        }
        
         
        [HttpGet]
        public IActionResult GetNotification()
        {
            try
            {
                return Json(_iIssueTrackingFacade.GetNotification(GetSession()));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
        
        [HttpPost]
        public IActionResult MarkReadNotification([FromQuery] string notId)
        {
            try
            {
                _iIssueTrackingFacade.MarkReadNotification(GetSession(), notId);
                return Json(new {message = "success"});
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
    }
}