using SoccerX.Application.Interfaces.FootballApiManager;
using SoccerX.Application.Interfaces.RestSharp;
using SoccerX.Common.Configuration;
using SoccerX.Common.Constants;
using SoccerX.Infrastructure.Services.RestSharp;

namespace SoccerX.Infrastructure.Services.FootballApi
{
    public class FootballApiManager : IFootballApiManager
    {
        #region Field
        private readonly IRestClientManager _restClientManager;
        private readonly ApplicationSettings applicationSettings;
        #endregion

        #region Constructor
        public FootballApiManager(ApplicationSettings applicationSettings)
        {
            _restClientManager = new RestClientManager(applicationSettings.FootballApiSettings.FootballApiBaseUrl, null,
                new Dictionary<string, string>
                {
                    { SoccerXConstants.RapidapiHost, applicationSettings.FootballApiSettings.RapidApiHost },
                    { SoccerXConstants.RapidapiKey, applicationSettings.FootballApiSettings.RapidApiKey },
                    { SoccerXConstants.RapidApisportsKey, applicationSettings.FootballApiSettings.RapidApisportsKey },
                }
             );
            this.applicationSettings = applicationSettings;
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
