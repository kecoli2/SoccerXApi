using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IAuditLogRepository AuditLogRepository { get; }
        IBetSlipRepository BetSlipRepository { get; }
        ICityRepository CityRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICountryRepository CountryRepository { get; }
        IEmailVerificationRepository EmailVerificationRepository { get; }
        ILikeRepository LikeRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IReferralRewardRepository ReferralRewardRepository { get; }
        ISchedulerResultRepository SchedulerResultRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }
        ITeamRepository TamRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    }
}
