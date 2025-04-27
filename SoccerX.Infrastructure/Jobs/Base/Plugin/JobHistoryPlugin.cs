using Quartz;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Infrastructure.Jobs.Base.Plugin
{
    public class JobHistoryPlugin: QuartzBasePlugin, IJobListener
    {
        #region Field
        public string Name => "QuartzHistoryPlugin";
        #endregion

        #region Constructor
        public JobHistoryPlugin(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        #endregion

        #region Public Method
        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = new CancellationToken())
        {
            return WriteResultToScheduler(context, jobException, cancellationToken);
        }
        #endregion

        #region Private Method

        #endregion
    }
}
