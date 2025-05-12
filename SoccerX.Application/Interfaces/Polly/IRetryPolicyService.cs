using System.Threading.Tasks;
using System.Threading;
using System;

namespace SoccerX.Application.Interfaces.Polly
{
    /// <summary>
    /// Async işlemler için merkezi retry politikası uygular.
    /// </summary>
    public interface IRetryPolicyService
    {
        /// <summary>
        /// Varsayılan policy ile retry uygular.
        /// </summary>
        Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken = default);

        /// <summary>
        /// Belirttiğiniz sayıda retry ile action'u çalıştırır.
        /// </summary>
        /// <param name="maxRetryAttempts">En fazla kaç kez deneneceği (başarı olana kadar).</param>
        Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, int maxRetryAttempts, CancellationToken cancellationToken = default);

        Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default);
        Task ExecuteAsync(Func<CancellationToken, Task> action, int maxRetryAttempts, CancellationToken cancellationToken = default);

    }
}
