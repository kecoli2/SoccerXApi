using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;

namespace SoccerX.Infrastructure.Jobs.Base.Plugin
{
    public class QuartzBasePlugin
    {
        #region Field
        private readonly string _serverName;
        private readonly string? _version;
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        public QuartzBasePlugin(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null)
                currentAssembly = Assembly.GetCallingAssembly();

            _serverName = Dns.GetHostName();
            _version = currentAssembly.GetName().Version?.ToString();
        }

        #endregion

        #region Public Method

        public Task WriteResultToScheduler(IJobExecutionContext context, JobExecutionException? exception, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var schedulerResultRepository = scope.ServiceProvider.GetRequiredService<ISchedulerResultRepository>();
            var startDateTime = context.FireTimeUtc.LocalDateTime;
            var workingTime = context.JobRunTime;
            var finishDate = startDateTime.Add(workingTime);

            var result = new Schedulerresult
            {
                Jobdescription = context.JobDetail.Description,
                Jobgroup = context.JobDetail.Key.Group,
                Jobkey = context.JobDetail.Key.Name,
                Result = exception == null ? SchedulerResultEnum.Ok : SchedulerResultEnum.Error,
                Resultdetail = exception == null ? "Successful" : $"Message : {exception.Message} - Detail Message : {exception.InnerException?.Message}",
                Startdate = startDateTime,
                Enddate = finishDate,
                Workingtime = workingTime,
                Userid = context.JobDetail.JobDataMap.GetNullableGuidValue(QuartzConstant.JobUserId)
            };

            schedulerResultRepository.AddAsync(result);
            return schedulerResultRepository.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
