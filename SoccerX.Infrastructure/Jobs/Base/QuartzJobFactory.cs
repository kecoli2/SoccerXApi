using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public class QuartzJobFactory: IJobFactory
    {
        #region Field
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region Public Method
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            // Yeni bir scope oluştur
            var scope = _serviceProvider.CreateScope();

            try
            {
                var jobType = bundle.JobDetail.JobType;
                // Job'ı scope'dan resolve et
                var job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;

                // Job dispose edilirken scope'u da dispose etmek için
                return new ScopedJob(scope, job);
            }
            catch
            {
                // Hata durumunda scope'u temizle
                scope.Dispose();
                throw;
            }
        }
        public void ReturnJob(IJob job)
        {
            // ScopedJob'ı dispose et
            (job as IDisposable)?.Dispose();
        }
        #endregion

        #region Private Method
        #endregion
    }

    internal class ScopedJob : IJob, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly IJob _innerJob;

        public ScopedJob(IServiceScope scope, IJob innerJob)
        {
            _scope = scope;
            _innerJob = innerJob;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return _innerJob.Execute(context);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
