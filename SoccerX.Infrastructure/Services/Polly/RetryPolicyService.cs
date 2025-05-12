using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using SoccerX.Application.Interfaces.Polly;
using SoccerX.Common.Configuration;

namespace SoccerX.Infrastructure.Services.Polly
{
    public class RetryPolicyService : IRetryPolicyService
    {
        #region Field
        private readonly RetrySettings _settings;
        private readonly ILogger<RetryPolicyService> _logger;

        #endregion

        #region Constructor
        public RetryPolicyService(IOptions<ApplicationSettings> options, ILogger<RetryPolicyService> logger)
        {
            _settings = options.Value.RetrySetting;
            _logger = logger;
        }
        #endregion

        #region Public Method
        public Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken = default) => ExecuteAsync(action, _settings.MaxRetryAttempts, cancellationToken);

        public Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default) => ExecuteAsync(action, _settings.MaxRetryAttempts, cancellationToken);

        public Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, int maxRetryAttempts, CancellationToken cancellationToken = default)
        {
            var policy = CreatePolicy(maxRetryAttempts);
            return policy.ExecuteAsync(ct => action(ct), cancellationToken);
        }

        public Task ExecuteAsync(Func<CancellationToken, Task> action, int maxRetryAttempts, CancellationToken cancellationToken = default)
        {
            var policy = CreatePolicy(maxRetryAttempts);
            return policy.ExecuteAsync(ct => action(ct), cancellationToken);
        }
        #endregion

        #region Private Method
        private AsyncRetryPolicy CreatePolicy(int maxRetries)
        {
            return Policy
                .Handle<Exception>(ex => _settings.HandleAllExceptions || ex.GetType() == _settings.HandledExceptionType)
                .WaitAndRetryAsync( retryCount: maxRetries, sleepDurationProvider: retryAttempt => TimeSpan.FromMilliseconds( _settings.InitialBackoffMs * Math.Pow(_settings.BackoffExponent, retryAttempt - 1)),
                    onRetry: (exception, timespan, retryCount, ctx) =>
                    {
                        _logger.LogWarning(exception, "Retry {RetryCount}/{MaxRetries} after {Delay}ms due to {Exception}.", retryCount, maxRetries, timespan.TotalMilliseconds, exception.GetType().Name);
                    });
        }
        #endregion       
    }
}
