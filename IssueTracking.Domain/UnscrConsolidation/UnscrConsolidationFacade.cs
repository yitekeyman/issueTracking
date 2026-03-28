using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;

namespace IssueTracking.Domain.UnscrConsolidation
{
    public interface IUNSCRConsolidationFacade
    {
        Task<List<UnscrConsolidatedList>> SearchAsync(UserSession session,string searchTerm);
        UnscrConsolidatedList GetUNSCRConsolidatedList(UserSession session, long id);
    }
    public class UnscrConsolidationFacade: IUNSCRConsolidationFacade
    {
        private readonly IUNSCRConsolidationServices _UNSCRConsolidationServices;

        public UnscrConsolidationFacade(IUNSCRConsolidationServices UNSCRConsolidationServices)
        {
            this._UNSCRConsolidationServices = UNSCRConsolidationServices;
        }

        public async Task<List<UnscrConsolidatedList>> SearchAsync(UserSession session, string searchTerm)
        {
            _UNSCRConsolidationServices.SetSession(session);
            return await _UNSCRConsolidationServices.SearchAsync(searchTerm);
        }

        public UnscrConsolidatedList GetUNSCRConsolidatedList(UserSession session, long id)
        {
            _UNSCRConsolidationServices.SetSession(session);
            return _UNSCRConsolidationServices.GetUNSCRConsolidatedList(id);
        }
    }
}