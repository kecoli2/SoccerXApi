using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Redis;
using SoccerX.Infrastructure.Caching;
using SoccerX.Infrastructure.Jobs.Base;

namespace SoccerX.Infrastructure.ServiceInstaller
{
    public static class InfrastructureInstaller
    {
     
        #region Public Method

        public static void InitiliazeInfrastructureServices(this IServiceCollection service)
        {
            RegisterRedis(service);
            RegisterQuartz(service);
            
        }
        #endregion

        #region Private Method
        private static void RegisterQuartz(IServiceCollection service)
        {
            service.AddSingleton<IQuartzManager, QuartzManager>();
            service.AddScoped<IQuartzJobCreater, QuartzJobCreater>();
        }

        private static void RegisterRedis(IServiceCollection service)
        {
            service.AddSingleton<IRedisCacheService, RedisCacheService>();
        }

        #endregion

    }
}
