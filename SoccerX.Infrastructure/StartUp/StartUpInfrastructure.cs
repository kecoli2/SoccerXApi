using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Redis;
using SoccerX.Infrastructure.Caching;
using SoccerX.Infrastructure.Jobs.Base;

namespace SoccerX.Infrastructure.StartUp
{
    public static class StartUpInfrastructure
    {
     
        #region Public Method

        public static IServiceCollection AddDependcyCollectionInfrastructure(this IServiceCollection service)
        {
            return service
                .RegisterRedis()
                .RegisterQuartz();
        }
        #endregion

        #region Private Method
        private static IServiceCollection RegisterQuartz(this IServiceCollection service)
        {
            return service
                .AddSingleton<IQuartzManager, QuartzManager>()
                .AddScoped<IQuartzJobCreater, QuartzJobCreater>();
        }

        private static IServiceCollection RegisterRedis(this IServiceCollection service)
        {
            return service.AddSingleton<IRedisCacheService, RedisCacheService>();
        }

        #endregion

    }
}
