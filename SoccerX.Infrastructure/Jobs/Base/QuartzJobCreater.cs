using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Common.Base.Quartz;
using SoccerX.Common.Enums;
using Quartz;
using SoccerX.Common.Attributes;
using SoccerX.Common.Extensions;
using SoccerX.Infrastructure.Caching.Memory;
using System.Reflection;
using SoccerX.Common.Base.Quartz.Models;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public class QuartzJobCreater : IQuartzJobCreater, IQuartzJobCreaterExtension
    {
        #region Field
        private readonly QuartzManager _scheduler;
        private string _culture = "tr-TR";
        private JobBuilder? _jobBuilder;
        private TriggerPriorityEnum TriggerPriority { get; set; } = TriggerPriorityEnum.Middle;
        private TriggerKey? TriggerKeyName { get; set; }
        private Guid? _userId = null;
        private DateTimeOffset? _startDateTime = null;
        private DateTimeOffset? _endDateTime = null;
        private string? _cronExpression = null;
        #endregion

        #region Constructor
        public QuartzJobCreater(IQuartzManager scheduler)
        {
            _scheduler = (QuartzManager)scheduler;
            PrepareLoadJob();
        }
        #endregion

        #region Public Method
        public IQuartzJobCreaterExtension CreateJob<T>() where T : IBaseJob
        {
            var jobAttribute = typeof(T).GetAttributeValue((JobAttributes dna) => dna);
            _jobBuilder = JobBuilder.Create(typeof(T));
            var jobKey = new JobKey(Guid.NewGuid().ToString(), jobAttribute.JobCategory.GetHashCode().ToString());
            _jobBuilder.WithIdentity(jobKey);
            _jobBuilder.WithDescription(jobAttribute.JobDescription);
            TriggerPriority = TriggerPriorityEnum.Middle;
            return this;
        }

        public IQuartzJobCreaterExtension Create(Type jobType)
        {
            var jobAttribute = jobType.GetAttributeValue((JobAttributes dna) => dna);
            _jobBuilder = JobBuilder.Create(jobType);
            var jobKey = new JobKey(Guid.NewGuid().ToString(), jobAttribute.JobCategory.GetHashCode().ToString());
            _jobBuilder.WithIdentity(jobKey);
            _jobBuilder.WithDescription(jobAttribute.JobDescription);
            TriggerPriority = TriggerPriorityEnum.Middle;
            return this;
        }

        public IQuartzJobCreaterExtension Create(JobKeyEnum jobKeyEnum)
        {
            var jobAttribute = FindJob(jobKeyEnum).GetAttributeValue((JobAttributes dna) => dna);
            _jobBuilder = JobBuilder.Create(FindJob(jobKeyEnum));
            var jobKey = new JobKey(Guid.NewGuid().ToString(), jobAttribute.JobCategory.GetHashCode().ToString());
            _jobBuilder.WithIdentity(jobKey);
            _jobBuilder.WithDescription(jobAttribute.JobDescription);
            TriggerPriority = TriggerPriorityEnum.Middle;
            return this;
        }

        public IQuartzJobCreaterExtension SetJobKey(string key)
        {
            _jobBuilder?.WithIdentity(new JobKey(Guid.NewGuid().ToString(), JobCategoryEnum.PublicJob.GetHashCode().ToString()));
            return this;
        }

        public IQuartzJobCreaterExtension SetJobKey(string key, JobCategoryEnum category)
        {
            _jobBuilder?.WithIdentity(new JobKey(Guid.NewGuid().ToString(), category.GetHashCode().ToString()));
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(object criteria)
        {
            IDictionary<string, object> data = new Dictionary<string, object>
            {
                { QuartzConstant.JobCriteria, criteria }
            };
            _jobBuilder?.UsingJobData(new JobDataMap(data));
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(string key, string value)
        {
            _jobBuilder?.UsingJobData(key, value);
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(string key, int value)
        {
            _jobBuilder?.UsingJobData(key, value);
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(string key, bool value)
        {
            _jobBuilder?.UsingJobData(key, value);
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(string key, float value)
        {
            _jobBuilder?.UsingJobData(key, value);
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(string key, double value)
        {
            _jobBuilder?.UsingJobData(key, value);
            return this;
        }

        public IQuartzJobCreaterExtension SetCriteria(string key, long value)
        {
            _jobBuilder?.UsingJobData(key, value);
            return this;
        }

        public IQuartzJobCreaterExtension SetDescription(string description)
        {
            _jobBuilder?.WithDescription(description);
            return this;
        }

        public IQuartzJobCreaterExtension SetRecovry(bool recovry)
        {
            _jobBuilder?.RequestRecovery(recovry);
            return this;
        }

        public IQuartzJobCreaterExtension SetDurably(bool durably)
        {
            _jobBuilder?.StoreDurably(durably);
            return this;
        }

        public IQuartzJobCreaterExtension SetPriority(TriggerPriorityEnum priority)
        {
            TriggerPriority = priority;
            return this;
        }

        public IQuartzJobCreaterExtension SetCulture(string culture)
        {
            _culture = culture ?? "tr-TR";
            return this;
        }

        public IQuartzJobCreaterExtension SetTriggerKey(string triggerKeyName)
        {
            TriggerKeyName = new TriggerKey(triggerKeyName, TriggerCategoryEnums.Default.GetHashCode().ToString());
            return this;
        }

        public IQuartzJobCreaterExtension SetTriggerKey(string triggerKeyName, string group)
        {
            TriggerKeyName = new TriggerKey(triggerKeyName, group);
            return this;
        }

        public IQuartzJobCreaterExtension SetUserId(Guid? id)
        {
            _userId = id;
            return this;
        }

        public IQuartzJobCreaterExtension StartDate(DateTimeOffset startDate)
        {
            _startDateTime = startDate;
            return this;
        }

        public IQuartzJobCreaterExtension EndDate(DateTimeOffset endDate)
        {
            _endDateTime = endDate;
            return this;
        }

        public IQuartzJobCreaterExtension SetCronExpression(string cronExpression)
        {
            _cronExpression = cronExpression;
            return this;
        }

        public async Task<JobDetailModel> Start()
        {
            try
            {
                var jobDetail = _jobBuilder?.Build();
                TriggerBuilder? triggerBuilder = null;
                if (_userId != null)
                    jobDetail?.JobDataMap.Put(QuartzConstant.JobUserId, _userId?.ToString());
                jobDetail?.JobDataMap.Put(QuartzConstant.JobCulture, _culture);

                if (_cronExpression != null)
                {
                    var startDate = _startDateTime ?? DateTime.Now;
                    if (_endDateTime == null || _endDateTime <= startDate)
                    {
                        _endDateTime = new DateTimeOffset(startDate.Date.Year, startDate.Date.Month, startDate.Date.Day,
                            23, 59, 59, TimeSpan.Zero);
                    }
                    else
                    {
                        _endDateTime = new DateTimeOffset(_endDateTime!.Value.Date.Year, _endDateTime!.Value.Date.Month,
                            _endDateTime!.Value.Date.Day, 23, 59, 59, TimeSpan.Zero);
                    }

                    triggerBuilder = TriggerBuilder.Create()
                        .StartAt(startDate)
                        .WithIdentity(TriggerKeyName ?? new TriggerKey(Guid.NewGuid().ToString(),
                            TriggerCategoryEnums.Default.GetHashCode().ToString()))
                        .WithDescription(jobDetail?.Description + "-Trigger")
                        .EndAt(_endDateTime)
                        .WithPriority(TriggerPriority.GetHashCode())
                        .WithSchedule(CronScheduleBuilder.CronSchedule(_cronExpression));
                }

                triggerBuilder ??= TriggerBuilder.Create()
                    .WithIdentity(TriggerKeyName ??
                                  new TriggerKey(Guid.NewGuid().ToString(),
                                      TriggerCategoryEnums.Default.GetHashCode().ToString()))
                    .WithDescription(jobDetail?.Description + "-Trigger")
                    .WithPriority(TriggerPriority.GetHashCode())
                    .StartNow();

                var detail = await _scheduler.GetScheduler().ScheduleJob(jobDetail!, triggerBuilder.Build());
                return CastToJobDetailModel(jobDetail!, detail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        #endregion

        #region Private Method

        private Type FindJob(JobKeyEnum jobEnum)
        {
            return MemoryCacheObjects.JobListCache[jobEnum];
        }

        private void PrepareLoadJob()
        {
            if (MemoryCacheObjects.JobListCache.Count != 0)
            {
                return;
            }
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                // Sınıf üzerinde MyCustomAttribute attribute'ının olup olmadığını kontrol et
                var attribute = type.GetCustomAttributes(typeof(JobAttributes), false)
                    .Cast<JobAttributes>()
                    .FirstOrDefault();

                if (attribute == null) continue;
                // Enum değerine göre dictionary'ye ekleyelim
                MemoryCacheObjects.JobListCache.TryAdd(attribute.JobKey, type);
            }
        }

        private JobDetailModel CastToJobDetailModel(IJobDetail jobDetail, DateTimeOffset? dateTimeOffset = null)
        {
            return new JobDetailModel(jobDetail.Key.Name, jobDetail.Key.Group, jobDetail.Description,
                jobDetail.JobDataMap.WrappedMap, jobDetail.Durable, jobDetail.PersistJobDataAfterExecution,
                jobDetail.ConcurrentExecutionDisallowed, jobDetail.RequestsRecovery, dateTimeOffset);
        }
        #endregion
    }
}
