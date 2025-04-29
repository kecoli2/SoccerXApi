using Quartz.Impl.Matchers;
using Quartz;
using Quartz.Spi;
using SoccerX.Application.Interfaces.Quartz;

namespace SoccerX.API.HostedService
{
    public class QuartzHostedService: IHostedService
    {
        #region Field
        private readonly IQuartzManager _quartzManager;
        private readonly IJobFactory _jobFactory;
        #endregion

        #region Constructor
        public QuartzHostedService(IQuartzManager quartzManager)
        {
            _quartzManager = quartzManager;
        }
        #endregion

        #region Public Method
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _quartzManager.SetJobFactory(_jobFactory);
                await _quartzManager.StartAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _quartzManager.ShutdownAsync(cancellationToken);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
