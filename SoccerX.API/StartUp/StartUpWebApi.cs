using SoccerX.API.StartUp.Swagger;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.StartUp;
using SoccerX.Common.Configuration;
using SoccerX.DTO.StartUp;
using SoccerX.Infrastructure.StartUp;
using SoccerX.Persistence.StartUp;
using System.Reflection;
using System.Resources;

namespace SoccerX.API.StartUp
{
    public static class StartUpWebApi
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        /// <summary>
        /// All Dependcy Injection StartUp
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="settings">Application Settings</param>
        /// <returns></returns>
        public static IServiceCollection AddDependcyCollectionWebApi(this IServiceCollection services, ApplicationSettings settings)
        {
            var assembly = Assembly.Load("SoccerX.Common");



            //builder.Services.AddSingleton<ResourceManager>(_ =>
            //    new ResourceManager(baseName, assembly));

            return services
                .AddSingleton(settings)
                .AddHttpClient()
                .AddSwagger()
                .AddSingleton<ResourceManager>(_ => new ResourceManager("SoccerX.Common.Properties.Resources", assembly))
                .AddDependcyCollectionDto()
                .AddDependcyCollectionInfrastructure()
                .AddDependcyCollectionPersistence(settings)
                .AddDependcyCollectionApplication(settings);
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Swagger DI
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns></returns>
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddEndpointsApiExplorer()
                    .AddSwaggerGen(c =>
                    {
                        c.OperationFilter<DefaultLanguageHeaderFilter>();
                    });
        }
        #endregion

    }
}
