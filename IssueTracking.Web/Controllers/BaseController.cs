using System;
using IssueTracking.Domain.Infrastructure;
using IssueTracking.Web.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracking.Web.Controllers
{
    public class BaseController:Controller
    {
        protected UserSession GetSession2()
        {
            return HttpContext.Session.GetSession<UserSession>("sessionInfo");
        }

        protected UserSession GetSession()
        {
            return GetSession2();
        }
        protected void AssertSession()
        {
            if (GetSession2()==null)
            {
                throw new InvalidOperationException("User not logged in or session has expired");
                
            }
        }
    }
}