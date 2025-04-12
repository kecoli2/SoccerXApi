using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Services.CountryService;
using SoccerX.Common.Configuration;

namespace SoccerX.Application.StartUp
{
    public static class StartUpSoccerXApplication
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        public static IServiceCollection AddDependcyCollectionApplication(this IServiceCollection service, ApplicationSettings settings)
        {
            return service
                .AddScoped<ICountriesService, CountriesService>();
            
        }
        #endregion

        #region Private Method
        #endregion
    }
}
