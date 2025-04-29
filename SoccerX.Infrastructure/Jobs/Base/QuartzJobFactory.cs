using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QuartzJobFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = _serviceScopeFactory.CreateScope();
            try
            {
                var job = (IJob)scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType);
                return new ScopedJobWrapper(job, scope);
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
            if (job is ScopedJobWrapper wrapper)
            {
                wrapper.Dispose();
            }
        }

        private class ScopedJobWrapper : IJob, IDisposable
        {
            private readonly IJob _innerJob;
            private readonly IServiceScope _scope;

            public ScopedJobWrapper(IJob innerJob, IServiceScope scope)
            {
                _innerJob = innerJob;
                _scope = scope;
            }

            public async Task Execute(IJobExecutionContext context)
            {
                await _innerJob.Execute(context);
            }

            public void Dispose()
            {
                _scope.Dispose();
            }
        }
    }
}
