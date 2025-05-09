using Microsoft.EntityFrameworkCore.Storage;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        #region Field
        public IAuditLogRepository AuditLogRepository { get; }
        public IBetSlipRepository BetSlipRepository { get; }
        public ICityRepository CityRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IEmailVerificationRepository EmailVerificationRepository { get; }
        public ILikeRepository LikeRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IReferralRewardRepository ReferralRewardRepository { get; }
        public ISchedulerResultRepository SchedulerResultRepository { get; }
        public ISubscriptionRepository SubscriptionRepository { get; }
        public ITeamRepository TamRepository { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IUserRepository UserRepository { get; }

        private readonly SoccerXDbContext _context;
        private IDbContextTransaction? _transaction;
        #endregion

        #region Constructor
        public UnitOfWork(IAuditLogRepository auditLogRepository, IBetSlipRepository betSlipRepository, ICityRepository cityRepository, ICommentRepository commentRepository, ICountryRepository countryRepository, IEmailVerificationRepository emailVerificationRepository, ILikeRepository likeRepository, INotificationRepository notificationRepository, IPaymentRepository paymentRepository, IReferralRewardRepository referralRewardRepository, ISchedulerResultRepository schedulerResultRepository, ISubscriptionRepository subscriptionRepository, ITeamRepository tamRepository, ITransactionRepository transactionRepository, IUserRepository userRepository, SoccerXDbContext context)
        {
            AuditLogRepository = auditLogRepository;
            BetSlipRepository = betSlipRepository;
            CityRepository = cityRepository;
            CommentRepository = commentRepository;
            CountryRepository = countryRepository;
            EmailVerificationRepository = emailVerificationRepository;
            LikeRepository = likeRepository;
            NotificationRepository = notificationRepository;
            PaymentRepository = paymentRepository;
            ReferralRewardRepository = referralRewardRepository;
            SchedulerResultRepository = schedulerResultRepository;
            SubscriptionRepository = subscriptionRepository;
            TamRepository = tamRepository;
            TransactionRepository = transactionRepository;
            UserRepository = userRepository;
            _context = context;
        }
        #endregion

        #region Public Method
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction ??= await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction başlatılmadı.");

            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            await DisposeTransactionAsync();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction başlatılmadı.");
            await _transaction.RollbackAsync(cancellationToken);
            await DisposeTransactionAsync();            
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Private Method
        private async Task DisposeTransactionAsync()
        {
            if (_transaction is null) return;
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        #endregion
    }
}
