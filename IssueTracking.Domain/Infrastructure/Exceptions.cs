using System;

namespace IssueTracking.Domain.Infrastructure
{
    public class AccessDeniedException:Exception
    {
        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}