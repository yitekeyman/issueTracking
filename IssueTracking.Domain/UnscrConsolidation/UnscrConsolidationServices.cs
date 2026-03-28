using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTracking.Domain.UnscrConsolidation
{
    
    public interface IUNSCRConsolidationServices
    {
        Task<List<UnscrConsolidatedList>> SearchAsync(string searchTerm);
        UnscrConsolidatedList GetUNSCRConsolidatedList(long id);

        void SetSession(UserSession session);
    }
    public class UnscrConsolidationServices:IUNSCRConsolidationServices
    {
        private readonly LIC_HRMSContext _context;
        private UserSession _session;
        
        public UnscrConsolidationServices(LIC_HRMSContext context)
        {
            _context = context;
        }

        public void SetSession(UserSession session)
        {
            _session = session;
        }
        
        public async Task<List<UnscrConsolidatedList>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await _context.UnscrConsolidatedList.ToListAsync();

            var normalizedTerm = searchTerm.Trim().ToLower();

            return await _context.UnscrConsolidatedList
                .Where(ml =>
                    ml.Code != null && ml.Code.ToLower().Contains(normalizedTerm) ||
                    ml.Name != null && ml.Name.ToLower().Contains(normalizedTerm) ||
                    ml.Title != null && ml.Title.ToLower().Contains(normalizedTerm) ||
                    ml.Designation != null && ml.Designation.ToLower().Contains(normalizedTerm) ||
                    ml.Dob != null && ml.Dob.ToLower().Contains(normalizedTerm) ||
                    ml.Pob != null && ml.Pob.ToLower().Contains(normalizedTerm) ||
                    ml.GoodQuality != null && ml.GoodQuality.ToLower().Contains(normalizedTerm) ||
                    ml.LowQuality != null && ml.LowQuality.ToLower().Contains(normalizedTerm) ||
                    ml.Nationality != null && ml.Nationality.ToLower().Contains(normalizedTerm) ||
                    ml.PassportNo != null && ml.PassportNo.ToLower().Contains(normalizedTerm) ||
                    ml.NationalId != null && ml.NationalId.ToLower().Contains(normalizedTerm) ||
                    ml.Address != null && ml.Address.ToLower().Contains(normalizedTerm) ||
                    ml.ListedOn != null && ml.ListedOn.ToLower().Contains(normalizedTerm) ||
                    ml.OtherInfo != null && ml.OtherInfo.ToLower().Contains(normalizedTerm))
                .ToListAsync();
            
        }

        public UnscrConsolidatedList GetUNSCRConsolidatedList(long id)
        {
            return _context.UnscrConsolidatedList.First(e => e.Id == id);
        }
    }
    }
