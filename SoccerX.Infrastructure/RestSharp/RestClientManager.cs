using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RestSharp;
using SoccerX.Application.Interfaces.RestSharp;
using SoccerX.Common.Shared.Model;

namespace SoccerX.Infrastructure.RestSharp
{
    public class RestClientManager: IRestClientManager
    {
        #region Field
        private readonly RestClient _client;
        private readonly ILogger<RestClientManager>? _logger;
        private Dictionary<string, string>? _defaultHeaders;
        private readonly AsyncRetryPolicy _retryPolicy;
        #endregion

        #region Constructor
        public RestClientManager(string baseUrl, ILogger<RestClientManager>? logger, Dictionary<string, string>? defaultHeaders = null)
        {
            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromSeconds(10)
            };

            //_retryPolicy = Policy<RestResponse>
            //    .Handle<HttpRequestException>()
            //    .OrResult(r => (int)r.StatusCode >= 500)
            //    .WaitAndRetryAsync(3, retryAttempt =>
            //        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });

            _client = new RestClient(options);
            _logger = logger;
            _defaultHeaders = defaultHeaders;
        }

        #endregion

        #region Public Method
        public async Task<RestClientApiResponse<T>> GetAsync<T>(string endpoint, object? queryParams = null, Dictionary<string, string>? headers = null)
        {
            var request = new RestRequest(endpoint, Method.Get);
            return await ExecuteAsync<T>(request, queryParams, headers);
        }

        public async Task<RestClientApiResponse<T>> PostAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = new RestRequest(endpoint, Method.Post);
            return await ExecuteAsync<T>(request, null, headers, body);
        }

        public async Task<RestClientApiResponse<T>> PutAsync<T>(string endpoint, object? body = null, Dictionary<string, string>? headers = null)
        {
            var request = new RestRequest(endpoint, Method.Put);
            return await ExecuteAsync<T>(request, null, headers, body);
        }

        public async Task<RestClientApiResponse<T>> DeleteAsync<T>(string endpoint, object? queryParams = null, Dictionary<string, string>? headers = null)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            return await ExecuteAsync<T>(request, queryParams, headers);
        }

        public void AddDefaultHeaders(string key, string value)
        {
            _defaultHeaders ??= new Dictionary<string, string>();
            _defaultHeaders?.TryAdd(key, value);
        }

        #endregion

        #region Private Method
        private async Task<RestClientApiResponse<T>> ExecuteAsync<T>(RestRequest request, object? queryParams, Dictionary<string, string>? headers, object? body = null)
        {
            try
            {
                // Query parametreleri ekleme
                if (queryParams != null)
                {
                    foreach (var prop in queryParams.GetType().GetProperties())
                    {
                        request.AddQueryParameter(prop.Name, prop.GetValue(queryParams)?.ToString());
                    }
                }

                if (_defaultHeaders != null)
                {
                    foreach (var header in _defaultHeaders)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                    
                }

                // Header'ları ekleme
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }

                // Body ekleme
                if (body != null)
                {
                    request.AddJsonBody(body);
                }

                var response = await _retryPolicy.ExecuteAsync(async () => await _client.ExecuteAsync<T>(request));

                return new RestClientApiResponse<T>
                {
                    IsSuccess = response.IsSuccessful,
                    Data = response.Data,
                    ErrorMessage = response.ErrorMessage ?? response.Content,
                    StatusCode = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "RestSharp request failed");
                return new RestClientApiResponse<T>
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    StatusCode = 500
                };
            }
        }
        #endregion
    }
}
