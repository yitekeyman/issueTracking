using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IssueTracking.Datas.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IssueTracking.Domain.IssueTracking;

namespace IssueTracking.Domain.Infrastructure
{
    public class TimerScheduling:IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private LIC_HRMSContext _context;
        public TimerScheduling(ILogger<TimerScheduling> logger)
        {
            _logger = logger;
            _context = new LIC_HRMSContext();
  
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("timed background Service started");
            // _timer = new Timer(DoWork, null, Timeout.Infinite, Timeout.Infinite);
            SetupTimer();
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("timed background service is working");
            OnTime();
        }
        private  void SetupTimer()
        {

            TimeSpan notPeriodTime = new TimeSpan(7, 30, 0);
            DateTime now=DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, notPeriodTime.Hours, notPeriodTime.Minutes, 0, 0);
            if (now > firstRun)
            {
                firstRun = firstRun.AddDays(1);
            }

            TimeSpan timeToGo = firstRun - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(timeToGo.TotalSeconds), TimeSpan.FromHours(24));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("timed background service is stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void OnTime()
        {
            var currentDate = DateTime.Now;
            var endDate = currentDate.AddDays(5);
            var thisWeekIssue = _context.IssueAssigned
                .Where(i => i.AssignDate > currentDate.Ticks && i.AssignDate <= endDate.Ticks)
                .ToList();
            var notificationList = new List<IssueNotification>();

            foreach (var ia in thisWeekIssue)
            {
                var activeIssue = _context.IssuesList.FirstOrDefault(i => i.Id == ia.Id);
    
                if (activeIssue != null)
                {
                    var issue = _context.IssuesList.First(c => c.Id == ia.Id);
                    var employees = _context.Account.Where(a => a.EmployeeId == ia.AssignedTo && a.Status == true).ToList();
        
                    foreach (var employee in employees)
                    {
                        var notif = new IssueNotification()
                        {
                            Id = Guid.NewGuid(),
                            NotificationTitle = activeIssue.IssueTitle,
                            NotificationDetail = $"You have been assigned to Issue : {activeIssue.IssueTitle}",
                            NotificationFrom = activeIssue.IssueRequestedBy,
                            NotificationTo = activeIssue.IssueRespondBy,
                            NotificationDate = new DateTime(activeIssue.IssueRequestedDate ?? 0).Ticks,
                            IssueId = ia.IssueId, 
                            Status = false,
                        };
                        notificationList.Add(notif);
                    }
                }
            }


            _context.IssueNotification.AddRange(notificationList);
            _context.SaveChanges();
        }
    }
}