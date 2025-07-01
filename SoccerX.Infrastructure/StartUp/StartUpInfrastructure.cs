using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using SoccerX.Application.Interfaces.Cache.Memory;
using SoccerX.Application.Interfaces.Cache.Redis;
using SoccerX.Application.Interfaces.FootballApiManager;
using SoccerX.Application.Interfaces.FootballApiManager.Services;
using SoccerX.Application.Interfaces.Polly;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Resources;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Application.Services.Email;
using SoccerX.Infrastructure.Jobs.Base;
using SoccerX.Infrastructure.Jobs.Base.Plugin;
using SoccerX.Infrastructure.Services.Caching;
using SoccerX.Infrastructure.Services.Caching.Memory;
using SoccerX.Infrastructure.Services.Email;
using SoccerX.Infrastructure.Services.FootballApi;
using SoccerX.Infrastructure.Services.FootballApi.Services;
using SoccerX.Infrastructure.Services.Polly;
using SoccerX.Infrastructure.Services.Resources;
using SoccerX.Infrastructure.Services.Security;

namespace SoccerX.Infrastructure.StartUp
{
    public static class StartUpInfrastructure
    {
     
        #region Public Method

        public static IServiceCollection AddDependcyCollectionInfrastructure(this IServiceCollection service)
        {
            return service
                .RegisterRedis()
                .RegisterQuartz()
                .RegisterFootBallApi()
                .AddScoped<IResourceManager, SoccerXResources>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IRetryPolicyService, RetryPolicyService>()
                .AddSingleton<IMemoryCacheService, MemoryCacheService>();
        }
        #endregion

        #region Private Method
        private static IServiceCollection RegisterQuartz(this IServiceCollection service)
        {
            service                
                .AddSingleton<IQuartzManager, QuartzManager>()
                .AddSingleton<JobHistoryPlugin>()                
                .AddSingleton<IJobFactory, QuartzJobFactory>()                
                .AddScoped<IQuartzJobCreater, QuartzJobCreater>()                                
                .AddScoped<IQuartzJobCreaterExtension, QuartzJobCreater>();


            var type = typeof(IJob);
            var lst = AppDomain.CurrentDomain.GetAssemblies()
                .Where(c => c.FullName?.Contains("SoccerX.Infrastructure") == true)
                .SelectMany(assembly => assembly.GetTypes())
                .Where(p => type.IsAssignableFrom(p)
                            && p is { IsInterface: false, IsAbstract: false }
                            && !p.Name.Contains("ScopedJobWrapper")); // ⬅️ Buraya dikkat!

            foreach (var implementationType in lst)
            {
                service.AddScoped(implementationType);
            }
            return service;
        }

        private static IServiceCollection RegisterRedis(this IServiceCollection service)
        {
            return service.AddSingleton<IRedisCacheService, RedisCacheService>();
        }

        private static IServiceCollection RegisterFootBallApi(this IServiceCollection service)
        {
            return service.AddTransient<IFootballApiManager, FootballApiManager>()
                .AddTransient<IFootballApiCoachsService, FootballApiCoachsService>()
                .AddTransient<IFootballApiCountriesService, FootballApiCountriesService>()
                .AddTransient<IFootballApiFixturesService, FootballApiFixturesService>()
                .AddTransient<IFootballApiInjuriesService, FootballApiInjuriesService>()
                .AddTransient<IFootballApiLeaguesService, FootballApiLeaguesService>()
                .AddTransient<IFootballApiOddsPreMatchService, FootballApiOddsPreMatchService>()
                .AddTransient<IFootballApiOddsService, FootballApiOddsService>()
                .AddTransient<IFootballApiPlayersService, FootballApiPlayersService>()
                .AddTransient<IFootballApiPredictionsService, FootballApiPredictionsService>()
                .AddTransient<IFootballApiSidelinedService, FootballApiSidelinedService>()
                .AddTransient<IFootballApiStandingsService, FootballApiStandingsService>()
                .AddTransient<IFootballApiTeamsService, FootballApiTeamsService>()
                .AddTransient<IFootballApiTransfersService, FootballApiTransfersService>()
                .AddTransient<IFootballApiTrophiesService, FootballApiTrophiesService>()
                .AddTransient<IFootballApiVenuesService, FootballApiVenuesService>();
        }
        #endregion

    }
}
