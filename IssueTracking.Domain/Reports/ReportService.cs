using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using IssueTracking.Datas.Entities;
using IssueTracking.Domain.Infrastructure;


namespace IssueTracking.Domain.Reports
{
    public interface IReportService
    {
        void SetSession(UserSession session);
        IssueByStatusStatisticsModel IssueStatusIssuePriorityStatistics(DateTime dateFrom, DateTime dateTo);
        BranchIssueStatusStatisticsModel BranchIssuePriorityStatistics(DateTime dateFrom, DateTime dateTo);
        IssueByStatusStatisticsModel IssueRaisedIssueStatusStatistics(DateTime dateFrom, DateTime dateTo);
        CancelledIssuesListModel CancelledIssuesListStat(DateTime dateFrom, DateTime dateTo);

    }

    public class ReportService : IReportService
    {
        private readonly LIC_HRMSContext _context;
        private UserSession _userSession;

        public ReportService(LIC_HRMSContext licHrmsContext)
        {
            _context = licHrmsContext;
        }

        public void SetSession(UserSession session)
        {
            _userSession = session;
        }

        private ReportHeaderModel ReportHeader(DateTime dateFrom, DateTime dateTo)
        {
            var reportHeader = new ReportHeaderModel();
            if (dateFrom == new DateTime(0001, 1, 1))
            {
                dateFrom = new DateTime(1970, 1, 1);
                reportHeader.DateFrom = dateFrom;
            }
            else
            {
                reportHeader.DateFrom = dateFrom;
            }

            if (dateTo == new DateTime(0001, 1, 1))
            {
                dateTo = DateTime.Now;
                reportHeader.DateTo = dateTo;
            }
            else
            {
                reportHeader.DateTo = dateTo;
            }

            reportHeader.ReportGeneratedOn = DateTime.Now;
            return reportHeader;
        }

        public IssueByStatusStatisticsModel IssueStatusIssuePriorityStatistics(DateTime dateFrom, DateTime dateTo)
        {
            var reportList = new IssueByStatusStatisticsModel();
            var reportHeader = ReportHeader(dateFrom, dateTo);
            reportHeader.ReportTitle = "Issue Priority with Issue Status Statistics";
            reportList.ReportHeader = reportHeader;

            //high 
            var high = new IssueByStatusStatistics()
            {
                Title = "High Priority",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssuePriority == 2 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssuePriority == 2 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssuePriority == 2 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssuePriority == 2 &&
                    (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(high);

            //medium
            var medium = new IssueByStatusStatistics()
            {
                Title = "Medium Priority",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssuePriority == 3 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssuePriority == 3 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssuePriority == 3 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssuePriority == 3 &&
                    (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(medium);

            //low
            var low = new IssueByStatusStatistics()
            {
                Title = "Low Priority",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssuePriority == 4 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssuePriority == 4 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssuePriority == 4 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssuePriority == 4 &&
                    (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(low);

            //normal
            var normal = new IssueByStatusStatistics()
            {
                Title = "Normal",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssuePriority == 1 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssuePriority == 1 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssuePriority == 1 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssuePriority == 1 &&
                    (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(normal);

            //total
            var total = new IssueByStatusStatistics()
            {
                Title = "Total",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && (i.IssuePriority >= 1 && i.IssuePriority <= 4) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && (i.IssuePriority >= 1 && i.IssuePriority <= 4) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && (i.IssuePriority >= 1 && i.IssuePriority <= 4) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    (i.IssuePriority >= 1 && i.IssuePriority <= 4) &&
                    (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                       i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueClosedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(total);
            return reportList;
        }

        public BranchIssueStatusStatisticsModel BranchIssuePriorityStatistics(DateTime dateFrom, DateTime dateTo)
        {
            var reportList = new BranchIssueStatusStatisticsModel();
            var reportHeader = ReportHeader(dateFrom, dateTo);
            reportHeader.ReportTitle = "Branch with Issue Priority, Issue Status Statistics";
            reportList.ReportHeader = reportHeader;
            var branches = _context.Branches.OrderBy(b => b.BaranchType).ToList();
            foreach (var br in branches)
            {
                long highOpen = 0;
                long highClosed = 0;
                long highPending = 0;
                long mediumOpen = 0;
                long mediumClosed = 0;
                long mediumPending = 0;
                long lowOpen = 0;
                long lowClosed = 0;
                long lowPending = 0;
                long normalOpen = 0;
                long normalClosed = 0;
                long normalPending = 0;

                long total = 0;

                var depSch = _context.DepartmentSchema.Where(d => d.BranchId == br.Id).ToList();
                foreach (var ds in depSch)
                {
                    highOpen += _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 2 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    highClosed += _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 2 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    highPending += _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 2 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    mediumOpen += _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 3 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    mediumClosed += _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 3 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    mediumPending += _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 3 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    lowOpen += _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 4 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    lowClosed += _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 4 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    lowPending += _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 4 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    normalOpen += _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 1 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    normalClosed += _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 1 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                    normalPending += _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 1 && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();

                    total += _context.IssuesList.Where(i =>
                        (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                        (i.IssuePriority >= 1 && i.IssuePriority <= 4) && i.BranchId == ds.Id &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count();
                }

                var report = new BranchIssueStatusStatistics()
                {
                    Title = br.BraName,
                    FirstCol = new IssueStatusModel()
                    {
                        Title = "High Priority", OpenIssues = highOpen, ClosedIssues = highClosed,
                        PendingIssues = highPending
                    },
                    SecondCol = new IssueStatusModel()
                    {
                        Title = "Medium Priority", OpenIssues = mediumOpen, ClosedIssues = mediumClosed,
                        PendingIssues = mediumPending
                    },
                    ThirdCol = new IssueStatusModel()
                    {
                        Title = "Low Priority", OpenIssues = lowOpen, ClosedIssues = lowClosed,
                        PendingIssues = lowPending
                    },
                    FourthCol = new IssueStatusModel()
                    {
                        Title = "Normal", OpenIssues = normalOpen, ClosedIssues = normalClosed,
                        PendingIssues = normalPending
                    },
                    Total = total
                };
                reportList.ReportList.Add(report);
            }

            //total
            var totalList = new BranchIssueStatusStatistics()
            {
                Title = "Total",
                FirstCol = new IssueStatusModel()
                {
                    Title = "High Priority",
                    OpenIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 2 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    ClosedIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 2 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    PendingIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 2 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),

                },
                SecondCol = new IssueStatusModel()
                {
                    Title = "Medium Priority",
                    OpenIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 3 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    ClosedIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 3 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    PendingIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 3 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                },
                ThirdCol = new IssueStatusModel()
                {
                    Title = "Low Priority",
                    OpenIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 4 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    ClosedIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 4 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    PendingIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 4 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                },
                FourthCol = new IssueStatusModel()
                {
                    Title = "Normal",
                    OpenIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 1 && i.IssuePriority == 1 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    ClosedIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 2 && i.IssuePriority == 1 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                    PendingIssues = _context.IssuesList.Where(i =>
                        i.IssueStatus == 3 && i.IssuePriority == 1 &&
                        i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                        i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                },
                Total = _context.IssuesList.Where(i =>
                    (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                    (i.IssuePriority >= 1 || i.IssuePriority <= 4) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(totalList);
            return reportList;
        }

        public IssueByStatusStatisticsModel IssueRaisedIssueStatusStatistics(DateTime dateFrom, DateTime dateTo)
        {
            var reportList = new IssueByStatusStatisticsModel();
            var reportHeader = ReportHeader(dateFrom, dateTo);
            reportHeader.ReportTitle = "Issue Raised with Issue Status Statistics";
            reportList.ReportHeader = reportHeader;
            var iraised = _context.IssueRaisedSystem.OrderBy(i => i.Name).ToList();

            //Antivirus
            var ant = new IssueByStatusStatistics()
            {
                Title = "Anti Virus & Security",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 8 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 8 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 8 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 8 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(ant);
            
            //BulkSMS
            var bs = new IssueByStatusStatistics()
            {
                Title = "BulkSMS",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 15 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 15 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 15 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 15 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(bs);
            
            //CH
            var ch = new IssueByStatusStatistics()
            {
                Title = "Computer Hardware",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 5 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 5 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 5 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 5 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(ch);
            
            //CS
            var cs = new IssueByStatusStatistics()
            {
                Title = "Computer Software",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 6 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 6 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 6 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 6 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(cs);

            //CW
            var cw = new IssueByStatusStatistics()
            {
                Title = "Company Website",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 10 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 10 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 10 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 10 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(cw);
            
            //HRMS
            var hrms = new IssueByStatusStatistics()
            {
                Title = "HRMS",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 9 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 9 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 9 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 9 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(hrms);
            
            //LCM
            var lcm = new IssueByStatusStatistics()
            {
                Title = "LCM",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 11 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 11 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 11 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 11 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(lcm);
            
            //MP
            var mp = new IssueByStatusStatistics()
            {
                Title = "Mapfre Portal",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 17 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 17 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 17 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 17 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(mp);
            
            //MSW
            var msw = new IssueByStatusStatistics()
            {
                Title = "Marine Single Window",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 16 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 16 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 16 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 16 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(msw);
            
            //NR
            var nr = new IssueByStatusStatistics()
            {
                Title = "Network Related",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 7 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 7 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 7 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 7 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(nr);
            
            //OCN
            var ocn = new IssueByStatusStatistics()
            {
                Title = "Online Claim Notification",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 12 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 12 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 12 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 12 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(ocn);
            
            //Other
            var other = new IssueByStatusStatistics()
            {
                Title = "Other",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 100 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 100 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 100 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 100 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                          i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(other);
            
            //Outlook
            var outl = new IssueByStatusStatistics()
            {
                Title = "Outlook & Email",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 21 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 21 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 21 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 21 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(outl);
            
            //Claim
            var clm = new IssueByStatusStatistics()
            {
                Title = "Premia Claims",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 2 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 2 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 2 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 2 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(clm);
            
             //PGL
            var pgl = new IssueByStatusStatistics()
            {
                Title = "Premia GL",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 3 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 3 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 3 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 3 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(pgl);

            //PRI
            var pri = new IssueByStatusStatistics()
            {
                Title = "Premia RI",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 4 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 4 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 4 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 4 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(pri);

            //PREMIA UW
            var puw = new IssueByStatusStatistics()
            {
                Title = "PREMIA UW",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 1 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 1 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 1 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 1 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                        i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(puw);
            
            //Printer
            var prt = new IssueByStatusStatistics()
            {
                Title = "Printer, Photocopy, scanner and related ",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 20 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 20 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 20 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 20 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(prt);

            //SHM
            var shm = new IssueByStatusStatistics()
            {
                Title = "SHM",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 13 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 13 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 13 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 13 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(shm);

            //TP
            var tp = new IssueByStatusStatistics()
            {
                Title = "Third Party",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 19 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 19 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 19 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 19 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(tp);
            
            //YC
            var yc = new IssueByStatusStatistics()
            {
                Title = "Yellow Card",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && i.IssueType.RaisedSystemId == 18 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && i.IssueType.RaisedSystemId == 18 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && i.IssueType.RaisedSystemId == 18 &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    i.IssueType.RaisedSystemId == 18 && (i.IssueStatus == 1 || i.IssueStatus == 2 ||
                                                         i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };
            reportList.ReportList.Add(yc);
            
            //total
            var total = new IssueByStatusStatistics()
            {
                Title = "Total",
                PendingIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 3 && ((i.IssueType.RaisedSystemId >= 1 && i.IssueType.RaisedSystemId <= 13) ||
                     (i.IssueType.RaisedSystemId >= 15 && i.IssueType.RaisedSystemId <= 21) ||
                    i.IssueType.RaisedSystemId == 100) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                OpenIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 1 && ((i.IssueType.RaisedSystemId >= 1 && i.IssueType.RaisedSystemId <= 13) ||
                     (i.IssueType.RaisedSystemId >= 15 && i.IssueType.RaisedSystemId <= 21) ||
                     i.IssueType.RaisedSystemId == 100) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                ClosedIssues = _context.IssuesList.Where(i =>
                    i.IssueStatus == 2 && ((i.IssueType.RaisedSystemId >= 1 && i.IssueType.RaisedSystemId <= 13) ||
                     (i.IssueType.RaisedSystemId >= 15 && i.IssueType.RaisedSystemId <= 21) ||
                     i.IssueType.RaisedSystemId == 100) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
                Total = _context.IssuesList.Where(i =>
                    ((i.IssueType.RaisedSystemId >= 1 && i.IssueType.RaisedSystemId <= 13) ||
                     (i.IssueType.RaisedSystemId >= 15 && i.IssueType.RaisedSystemId <= 21) ||
                     i.IssueType.RaisedSystemId == 100) &&
                    (i.IssueStatus == 1 || i.IssueStatus == 2 || i.IssueStatus == 3) &&
                    i.IssueRequestedDate >= reportHeader.DateFrom.Ticks &&
                    i.IssueRequestedDate <= reportHeader.DateTo.Ticks).Count(),
            };

            reportList.ReportList.Add(total);
            return reportList;
        }

        public CancelledIssuesListModel CancelledIssuesListStat(DateTime dateFrom, DateTime dateTo)
        {
            var reportList = new CancelledIssuesListModel();
            var reportHeader = ReportHeader(dateFrom, dateTo);
            reportHeader.ReportTitle = "Cancelled Issues List";
            reportList.ReportHeader = reportHeader;
            var delIss = _context.IssuesList
                .Where(d => d.IssueStatus == 4 &&
                            d.IssueClosedDate >= reportHeader.DateFrom.Ticks &&
                            d.IssueClosedDate <= reportHeader.DateTo.Ticks).ToList()
                .OrderBy(d => d.IssueRequestedDate);
            foreach (var dl in delIss)
            {
                var department = GetDepartment(dl.BranchId).DepartmentName;
                if (GetDepartment(dl.BranchId).BranchId != 10)
                    department = GetDepartment(dl.BranchId).BranchName;
                var issueType = GetIssueType(dl.IssueTypeId); // Call the GetIssueType method to retrieve the IssueType object
               
                var issueRaisedSystem = _context.IssueRaisedSystem.FirstOrDefault(i => i.Id == dl.IssueType.RaisedSystemId);
                var issueRaised = issueRaisedSystem != null ? issueRaisedSystem.Name : "N/A";
                
                var cancelReason = _context.IssueComments
                    .Where(ic => ic.IssueId == (Guid?)dl.Id)
                    .OrderByDescending(ic => ic.CommentDate)
                    .Select(ic => ic.IssueComment)
                    .FirstOrDefault();
                var report = new CancelledIssuesList()
                {
                    EmployeeId = dl.Id.ToString(),
                    IssueTitle = dl.IssueTitle,
                    IssueRaised = issueRaised,
                    IssueType = issueType != null ? issueType.Name : "N/A",
                    Ticket = dl.Ticket,
                    EmployeeName = GetEmployee(dl.IssueRequestedBy).FirstName + " " + GetEmployee(dl.IssueRequestedBy).FatherName,
                    Branch = department,
                    IssueDate = new DateTime(dl.IssueRequestedDate ?? 0),
                    CancelReason = cancelReason
                };

                reportList.ReportList.Add(report);
            }

            return reportList;
        }
        
        
        private DepartmentSchemaModel GetDepartment(Guid id)
        {
            var dept = _context.DepartmentSchema.First(e => e.Id == id);
            var ret = new DepartmentSchemaModel()
            {
                Id = dept.Id.ToString(),
                BranchId = dept.BranchId,
                DepartmentId = dept.DepartmentId,
                DepartmentName = _context.Department.First(d => d.Id == dept.DepartmentId).Name,
                BranchName = _context.Branches.First(b => b.Id == dept.BranchId).BraName
            };

            return ret;
        }
        public IssueTypeList GetIssueType(long? issueTypeId)
        {
            var issueType = _context.IssueTypeList.FirstOrDefault(it => it.Id == issueTypeId);
            return issueType;
        }
        
        private EmployeeModel GetEmployee(Guid id)
        {
            var employee = _context.Employee.First(e => e.Id == id);
            var ret = new EmployeeModel()
            {
                Id = employee.Id.ToString(),
                FirstName = employee.FirstName,
                FatherName = employee.FatherName,
                GrFatherName = employee.GrFatherName,
                EmpIdNo = employee.EmpIdNo,
                Username = _context.Account.First(e => e.EmployeeId == employee.Id).Username,
                PhoneNo = employee.Phone,
                Email = employee.Email
            };
            return ret;
        }
        
            

    }
}


