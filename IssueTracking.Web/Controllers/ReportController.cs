using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IssueTracking.Domain.Infrastructure;
using IssueTracking.Domain.Reports;


namespace IssueTracking.Web.Controllers
{
    public class ReportController: BaseController
    {
        static Dictionary<String, UserSession> sessions = new Dictionary<string, UserSession>();

        public static UserSession GetSession(string sid)
        {
            if (sessions.ContainsKey(sid))
                return sessions[sid];
            return null;
        }

        private IReportFacade _reportFacade;
        public ReportController(IReportFacade reportFacade)
        {
            _reportFacade = reportFacade;
        }
        
        [HttpGet]
        public IActionResult StatusIssuePriorityReport([FromQuery] DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var session = GetSession();
                return Json(_reportFacade.IssueStatusIssuePriorityStatistics(session, dateFrom, dateTo));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
        [HttpGet]
        public IActionResult BranchIssuePriorityReport([FromQuery] DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var session = GetSession();
                return Json(_reportFacade.BranchIssuePriorityStatistics(session, dateFrom, dateTo));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
        [HttpGet]
        public IActionResult IssueRaiseIssueStatusReport([FromQuery] DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var session = GetSession();
                return Json(_reportFacade.IssueRaisedIssueStatusStatistics(session, dateFrom, dateTo));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
        [HttpGet]
        public IActionResult CancelledIssuesListStatReport([FromQuery] DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var session = GetSession();
                return Json(_reportFacade.CancelledIssuesListStat(session, dateFrom, dateTo));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {message = e.Message});
            }
        }
    }
}