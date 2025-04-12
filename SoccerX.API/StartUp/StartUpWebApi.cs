using SoccerX.Application.StartUp;
using SoccerX.Common.Configuration;
using SoccerX.DTO.StartUp;
using SoccerX.Infrastructure.StartUp;
using SoccerX.Persistence.StartUp;

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
            return services
                .AddSingleton(settings)
                .AddSwagger()
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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(); // BU MUTLAKA OLMALI
            return services;
        }
        #endregion

    }
}
