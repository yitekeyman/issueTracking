using System;
using System.Collections.Generic;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using IssueTracking.Domain.IssueTracking;
using IssueTracking.Web.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracking.Web.Controllers
{
    public class IssueTrackingController:BaseController
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
            }
        }

        [HttpPost]
        public IActionResult EditIssueType([FromBody] IssueTypeList model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditIssueType(GetSession(),model);
                return StatusCode(200, new {message = true});
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
            }
        }
        [HttpPost]
        public IActionResult EditBasicIssueSolution([FromBody] BasicSolutionModel model)
        {
            if (!ModelState.IsValid) return Json(false);
            try
            {
                _iIssueTrackingFacade.EditBasicIssueSolution(GetSession(),model);
                return StatusCode(200, new {message = true});
            }
            catch (Exception e)
            {
                var stCode = 500;
                if (e.Message.Equals("Value cannot be null.\r\nParameter name: value"))
                    stCode = 400;
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
                return StatusCode(stCode, new { message = e.InnerException });
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
        
    }
}