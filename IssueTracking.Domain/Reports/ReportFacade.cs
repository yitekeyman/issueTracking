using System;
using IssueTracking.Domain.Infrastructure;


namespace IssueTracking.Domain.Reports
{
    public interface IReportFacade
    {
        IssueByStatusStatisticsModel IssueStatusIssuePriorityStatistics(UserSession session, DateTime dateFrom, DateTime dateTo);
        BranchIssueStatusStatisticsModel BranchIssuePriorityStatistics(UserSession session, DateTime dateFrom, DateTime dateTo);
        IssueByStatusStatisticsModel IssueRaisedIssueStatusStatistics(UserSession session, DateTime dateFrom, DateTime dateTo);
        CancelledIssuesListModel CancelledIssuesListStat(UserSession session, DateTime dateFrom, DateTime dateTo);

    }

    public class ReportFacade : IReportFacade
    {
        private IReportService _iReportService;

        public ReportFacade(IReportService iReportService)
        {
            _iReportService = iReportService;
        }

        public IssueByStatusStatisticsModel IssueStatusIssuePriorityStatistics(UserSession session, DateTime dateFrom, DateTime dateTo)
        {
            _iReportService.SetSession(session);
            return _iReportService.IssueStatusIssuePriorityStatistics(dateFrom, dateTo);
        }

        public BranchIssueStatusStatisticsModel BranchIssuePriorityStatistics(UserSession session, DateTime dateFrom, DateTime dateTo)
        {
            _iReportService.SetSession(session);
            return _iReportService.BranchIssuePriorityStatistics(dateFrom, dateTo);
        }

        public IssueByStatusStatisticsModel IssueRaisedIssueStatusStatistics(UserSession session, DateTime dateFrom, DateTime dateTo)
        {
            _iReportService.SetSession(session);
            return _iReportService.IssueRaisedIssueStatusStatistics(dateFrom, dateTo);
        }

        public CancelledIssuesListModel CancelledIssuesListStat(UserSession session, DateTime dateFrom, DateTime dateTo)
        {
            _iReportService.SetSession(session);
            return _iReportService.CancelledIssuesListStat(dateFrom, dateTo);
        }
    }
}