using SoccerX.Application.Interfaces.FootballApiManager;
using SoccerX.Application.Interfaces.FootballApiManager.Services;
using SoccerX.Application.Parameters.FotballApi.Parameters;
using SoccerX.Common.Constants;
using SoccerX.DTO.Responses.FootballApi;

namespace SoccerX.Infrastructure.Services.FootballApi.Services
{
    public class FootballApiCountriesService(IFootballApiManager footballApiManager) : IFootballApiCountriesService
    {
        #region Field        
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<FootBallApiResponse<FootBallApiCountriesResponse>?> GetCountriesAsync(CountriesParameters? parameters = null)
        {
            parameters?.IsValid();
            var response = await footballApiManager.GetClient(parameters).GetAsync<FootBallApiResponse<FootBallApiCountriesResponse>>(FootballApiConstant.FootballApi_Countries, parameters);
            if(response != null && response.IsSuccess)
            {
               return response.Data;
            }

            return new FootBallApiResponse<FootBallApiCountriesResponse>
            {
                
            };
        }
        #endregion

        #region Private Method
        #endregion        
    }
}
