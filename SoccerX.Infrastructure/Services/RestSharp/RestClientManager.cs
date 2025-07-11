using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RestSharp;
using SoccerX.Application.Interfaces.RestSharp;
using SoccerX.Common.Attributes;
using SoccerX.Common.Shared.Model;
using SoccerX.DTO.Responses.FootballApi;
using System.Reflection;

namespace SoccerX.Infrastructure.Services.RestSharp
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

        public async Task<RestClientApiResponse<T>> ExecuteAsync<T>(RestRequest request)
        {
            try
            {
                if (_defaultHeaders != null)
                {
                    foreach (var header in _defaultHeaders)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
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
                        var attr = prop.GetCustomAttribute<QueryNameAttribute>();
                        var key = attr?.Name ?? prop.Name;
                        var value = prop.GetValue(queryParams)?.ToString();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            request.AddQueryParameter(key, value);
                        }
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

                var response = await _retryPolicy.ExecuteAsync(async () => await _client.ExecuteAsync(request));

                bool isLogicalSuccess = true;
                string? logicalErrorMessage = null;

                T? deserializedData = default;

                try
                {
                    // JSON'u önce JObject olarak oku
                    var jObj = JObject.Parse(response.Content);

                    // "errors" varsa yakala
                    if (jObj.TryGetValue("errors", out var errorsToken) && errorsToken.Type == JTokenType.Object)
                    {
                        var errorsDict = errorsToken.ToObject<Dictionary<string, string>>();
                        if (errorsDict?.Count > 0)
                        {
                            isLogicalSuccess = false;
                            logicalErrorMessage = string.Join(" | ", errorsDict.Select(e => $"{e.Key}: {e.Value}"));
                        }
                    }

                    // Sonra asıl data'yı deserialize et
                    deserializedData = JsonConvert.DeserializeObject<T>(response.Content);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Deserialization or error parsing failed");
                    isLogicalSuccess = false;
                    logicalErrorMessage = ex.Message;
                }

                return new RestClientApiResponse<T>
                {
                    IsSuccess = response.IsSuccessful && isLogicalSuccess,
                    Data = deserializedData,
                    ErrorMessage = logicalErrorMessage ?? response.ErrorMessage ?? response.Content,
                    StatusCode = (int)response.StatusCode
                };

                //var response = await _retryPolicy.ExecuteAsync(async () => await _client.ExecuteAsync<T>(request));

                //return new RestClientApiResponse<T>
                //{
                //    IsSuccess = response.IsSuccessful,
                //    Data = response.Data,
                //    ErrorMessage = response.ErrorMessage ?? response.Content,
                //    StatusCode = (int)response.StatusCode
                //};
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
