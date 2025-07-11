using SoccerX.Application.Interfaces.RestSharp;

namespace SoccerX.Application.Interfaces.FootballApiManager
{
    public interface IFootballApiManager
    {
        IRestClientManager GetClient(IFotballApiParameters? parameters);
    }
}
