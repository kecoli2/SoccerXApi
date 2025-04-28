using Quartz;
using Quartz.Impl;
using SoccerX.Common.Configuration;
using System.Collections.Specialized;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Common.Base.Quartz.Models;
using SoccerX.Infrastructure.Jobs.Base.Plugin;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public class QuartzManager : IQuartzManager
    {
        #region Field
        private readonly QuartzSettings _settings;
        private readonly IScheduler _scheduler;
        private IJobFactory _jobFactory;
        #endregion

        #region Constructor
        public QuartzManager(ApplicationSettings options, JobHistoryPlugin jobHistoryPlugin, IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
            _settings = options.Quartz;
            var props = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "SoccerXScheduler" },
                { "quartz.scheduler.instanceId", "AUTO" },
                { "quartz.serializer.type", "json" }
            };

            if (_settings.JobStoreType == "Database")
            {
                props.Add("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
                props.Add("quartz.jobStore.useProperties", "false");
                props.Add("quartz.jobStore.dataSource", "default");
                props.Add("quartz.jobStore.tablePrefix", "qrtz_");
                props.Add("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.PostgreSQLDelegate, Quartz");
                props.Add("quartz.dataSource.default.provider", "Npgsql");
                props.Add("quartz.dataSource.default.connectionString", options.GetDatabaseConnectionString());
                props.Add("quartz.threadPool.threadCount", _settings.MaxConcurrency.ToString());
                props.Add("quartz.threadPool.threadPriority", "5");
                props.Add("quartz.jobStore.clustered", "true");
                props.Add("quartz.jobStore.clusterCheckinInterval", "20000");
            }
            else
            {
                props.Add("quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz");
            }

            props.Add("quartz.jobStore.misfireThreshold", _settings.MisfireThreshold.ToString());


            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(props);
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.ListenerManager.AddJobListener(jobHistoryPlugin, GroupMatcher<JobKey>.AnyGroup());
            _scheduler.JobFactory = jobFactory;
        }
        #endregion

        #region Public Method
        public async Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_settings.StartOnStartup)
            {
                await _scheduler.Start(cancellationToken);
            }
        }

        public async Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _scheduler.Shutdown(waitForJobsToComplete: true, cancellationToken);
        }

        public async Task PauseAllJobs(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _scheduler.PauseAll(cancellationToken);
        }

        public async Task PauseJob(string jobKey, string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _scheduler.PauseJob(JobKey.Create(jobKey, jobGroup), cancellationToken);
        }

        public async Task DeleteAllJobs(NameValueCollection jobKeys, CancellationToken cancellationToken = default(CancellationToken))
        {
            var lisKeys = (from jobKey in jobKeys.AllKeys where jobKey != null select JobKey.Create(jobKey, jobKeys[jobKey])).ToList();
            await _scheduler.DeleteJobs(lisKeys, cancellationToken);
        }

        public async Task<bool> CheckExistJob(string jobKey, string jobGroup, CancellationToken cancellationToken)
        {
            return await _scheduler.CheckExists(JobKey.Create(jobKey, jobGroup), cancellationToken);
        }

        public async Task<JobDetailModel?> GetJobDetail(string jobKey, string jobGroup, CancellationToken cancellationToken)
        {
            var detail = await _scheduler.GetJobDetail(JobKey.Create(jobKey, jobGroup), cancellationToken);
            if (detail == null)
                return null;
            return new JobDetailModel(detail.Key.Name,
                detail.Key.Group,
                detail.Description,
                detail.JobDataMap.WrappedMap,
                detail.Durable,
                detail.PersistJobDataAfterExecution,
                detail.ConcurrentExecutionDisallowed, detail.RequestsRecovery);
        }

        public async Task PauseTrigger(string triggerKey, CancellationToken cancellationToken)
        {
            await _scheduler.PauseTrigger(new TriggerKey(triggerKey), cancellationToken);
        }

        public async Task ResumeTrigger(string triggerKey, CancellationToken cancellationToken)
        {
            await _scheduler.ResumeTrigger(new TriggerKey(triggerKey), cancellationToken);
        }

        public Task ResumeErrorTrigger(CancellationToken cancellationToken)
        {
            try
            {
                var triggers = _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup(), cancellationToken);
                foreach (var trigger in triggers.Result)
                {
                    if (_scheduler.GetTriggerState(trigger, cancellationToken).Result != TriggerState.Error) continue;
                    var trig = _scheduler.GetTrigger(trigger, cancellationToken).Result;
                    if (trig != null)
                    {
                        _scheduler.RescheduleJob(trigger, trig!.Clone(), cancellationToken);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromCanceled(CancellationToken.None);
            }
            return Task.CompletedTask;
        }

        public async Task<bool> IsJobGroupPaused(string groupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _scheduler.IsJobGroupPaused(groupName, cancellationToken);
        }

        public void SetJobFactory(object factory)
        {
            _jobFactory = (IJobFactory)factory;
        }

        public IScheduler GetScheduler() => _scheduler;

        public IJobFactory JobFactory
        {
            set => _jobFactory = value; //geçici olarak değiştirildi
        }
        #endregion

        #region Private Method
        #endregion
    }
}
