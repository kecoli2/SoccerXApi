using SoccerX.Application.Interfaces.Quartz;

namespace SoccerX.API.HostedService
{
    public class QuartzHostedService: IHostedService
    {
        #region Field
        private readonly IQuartzManager _quartzManager;
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
            await _quartzManager.StartAsync(cancellationToken);
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
