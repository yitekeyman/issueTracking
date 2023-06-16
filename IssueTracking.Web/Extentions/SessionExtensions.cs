﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IssueTracking.Web.Extentions
{
    public static class SessionExtensions
    {
        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetSession<T>(this ISession session, string key)
        {
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}