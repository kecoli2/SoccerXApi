using Quartz;
using SoccerX.Common.Base.Quartz;
using SoccerX.Common.Base.Quartz.Criteria.Base;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public abstract class BaseJob<T> : IJob, IBaseJob where T : JobBaseCriteria
    {
        #region Field
        public IJobExecutionContext? JobContext { get; set; }
        public T? JobCriteria { get; private set; }
        public string Culture { get; private set; } = "tr-TR";
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public abstract Task Executing();
        public async Task Execute(IJobExecutionContext context)
        {
            JobContext = context;
            try
            {
                if (JobContext?.JobDetail.JobDataMap[QuartzConstant.JobCriteria] != null)
                {
                    JobCriteria = (T)Convert.ChangeType(JobContext.JobDetail.JobDataMap[QuartzConstant.JobCriteria], typeof(T));
                }

                if (JobContext?.JobDetail.JobDataMap[QuartzConstant.JobCulture] != null)
                {
                    Culture = (string)Convert.ChangeType(JobContext.JobDetail.JobDataMap[QuartzConstant.JobCulture], typeof(string));
                }
                await Executing();
            }
            catch (Exception ex)
            {
                throw new JobExecutionException("Job excetution error", ex, false);
            }            
        }
        #endregion

        #region Private Method

        #endregion
    }
}
