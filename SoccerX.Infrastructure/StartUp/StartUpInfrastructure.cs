using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Redis;
using SoccerX.Application.Interfaces.RestSharp;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Infrastructure.Caching;
using SoccerX.Infrastructure.Jobs.Base;
using SoccerX.Infrastructure.RestSharp;
using SoccerX.Infrastructure.Security;

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
                .AddScoped<IQuartzJobCreater, QuartzJobCreater>()
                .AddScoped<IJwtService, JwtService>()
                .AddSingleton<IRestClientManager>(new RestClientManager("http://google.com", null));
        }

        private static IServiceCollection RegisterRedis(this IServiceCollection service)
        {
            return service.AddSingleton<IRedisCacheService, RedisCacheService>();
        }

        #endregion

    }
}
