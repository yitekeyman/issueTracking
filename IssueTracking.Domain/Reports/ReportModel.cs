using System;
using System.Collections.Generic;


namespace IssueTracking.Domain.Reports
{
    public class ReportHeaderModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime ReportGeneratedOn { get; set; }
        public String ReportTitle { get; set; }
    }

    public class IssueByStatusStatistics
    {
        public string Title { get; set; }
        public long OpenIssues { get; set; }
        public long ClosedIssues { get; set; }
        public long PendingIssues { get; set; }
        public long Total { get; set; }
    }
    
    public class IssueByStatusStatisticsModel
    {
        public ReportHeaderModel ReportHeader { get; set; } = new ReportHeaderModel();
        public IList<IssueByStatusStatistics> ReportList  { get; set; }= new List<IssueByStatusStatistics>();
    }

    public class IssueStatusModel
    {
        public string Title { get; set; }
        public long OpenIssues { get; set; }
        public long ClosedIssues { get; set; }
        public long PendingIssues { get; set; }
    }

    public class BranchIssueStatusStatistics
        {
            public string Title { get; set; }
            public IssueStatusModel FirstCol { get; set; }= new IssueStatusModel();
            public IssueStatusModel SecondCol { get; set; }= new IssueStatusModel();
            public IssueStatusModel ThirdCol { get; set; }= new IssueStatusModel();
            public IssueStatusModel FourthCol { get; set; }= new IssueStatusModel();
            public long Total { get; set; }
        }

        public class BranchIssueStatusStatisticsModel
        {
            public ReportHeaderModel ReportHeader { get; set; }= new ReportHeaderModel();
            public IList<BranchIssueStatusStatistics> ReportList { get; set; }= new List<BranchIssueStatusStatistics>();
        }

        public class CancelledIssuesList
        {
            public string EmployeeId { get; set; }
            public string Ticket { get; set; }
            public string IssueTitle { get; set; }
            public string IssueRaised { get; set; }
            public string IssueType { get; set; }
            public string CancelReason { get; set; }
            public string Branch { get; set; }
            public string EmployeeName { get; set; }
            public DateTime IssueDate { get; set; }
        }

        public class CancelledIssuesListModel
        {
            public ReportHeaderModel ReportHeader { get; set; }= new ReportHeaderModel();
            public IList<CancelledIssuesList> ReportList { get; set; }= new List<CancelledIssuesList>();
        }

        public class DepartmentSchemaModel
        {
            public string Id { get; set; }
            public long BranchId { get; set; }
            public long DepartmentId { get; set; }
            public string BranchName { get; set; }
            public string DepartmentName { get; set; }
        }

        public class EmployeeModel
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public string GrFatherName { get; set; }
            public string EmpIdNo { get; set; }
            public string Username { get; set; }
            public string PhoneNo { get; set; }
            public string Email { get; set; }
        }
    }