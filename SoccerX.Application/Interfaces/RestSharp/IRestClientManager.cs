using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Common.Shared.Model;

namespace SoccerX.Application.Interfaces.RestSharp
{
    public interface IRestClientManager
    {
        Task<RestClientApiResponse<T>> GetAsync<T>(string endpoint, object? queryParams = null, Dictionary<string, string>? headers = null);
        Task<RestClientApiResponse<T>> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null);
        Task<RestClientApiResponse<T>> PutAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null);
        Task<RestClientApiResponse<T>> DeleteAsync<T>(string endpoint, object? queryParams = null, Dictionary<string, string>? headers = null);
        void AddDefaultHeaders(string key, string value);
    }
}
