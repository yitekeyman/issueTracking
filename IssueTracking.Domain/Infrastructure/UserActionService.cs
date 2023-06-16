using System;
using System.Linq;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.IssueTracking;

namespace IssueTracking.Domain.Infrastructure
{
    public interface IUserActionService
    {
        UserAction AddUserAction(UserSession session, IssueTrackingModel.UserActionType actionType);
    }
    public class UserActionService:IUserActionService
    {
        private readonly LIC_HRMSContext _context;

        public UserActionService(LIC_HRMSContext context)
        {
            _context = context;
        }
        
        public UserAction GetUserAction(long aid)
        {
            var ac = _context.UserAction.Where(a => a.Id == aid);
            if (ac.Any())
                return ac.First();
            return null;
        }
        public UserAction AddUserAction(UserSession session, IssueTrackingModel.UserActionType type)
        {
            var actionType = GetActionType((int)type);

            var user = _context.Account.First(u => u.Username.Equals(session.Username));

            var action = new UserAction
            {
                TimeStamp = DateTime.Now.Ticks,
                Username = user.Username,
                ActionTypeId= (int)type
                
            };
            _context.UserAction.Add(action);
            _context.SaveChanges();
            return action;
        }
        private ActionType GetActionType(int id)
        {
            return _context.ActionType.First(at => at.Id == id);
        }
    }
}