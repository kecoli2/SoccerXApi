using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using SoccerX.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public class QuartzManager : IQuartzManager
    {
        #region Field
        private readonly QuartzSettings _settings;
        private readonly IScheduler _scheduler;
        #endregion

        #region Constructor
        public QuartzManager(IOptions<QuartzSettings> options)
        {
            _settings = options.Value;
            var props = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "SoccerXScheduler" },
            };

            if (_settings.JobStoreType == "Database")
            {
                props.Add("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
                props.Add("quartz.dataSource.default.connectionString", _settings.ConnectionString);
                props.Add("quartz.dataSource.default.provider", "Npgsql");
            }
            else
            {
                props.Add("quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz");
            }

            props.Add("quartz.jobStore.misfireThreshold", _settings.MisfireThreshold.ToString());
            props.Add("quartz.scheduler.threadPool.threadCount", _settings.MaxConcurrency.ToString());


            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(props);
            _scheduler = schedulerFactory.GetScheduler().Result;
        }
        #endregion

        #region Public Method
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_settings.StartOnStartup)
            {
                await _scheduler.Start(cancellationToken);
            }
        }

        public async Task ShutdownAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(waitForJobsToComplete: true, cancellationToken);
        }

        public IScheduler GetScheduler() => _scheduler;
        #endregion

        #region Private Method
        #endregion
    }
}
