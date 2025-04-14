using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Common.Configuration;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interceptors;
using SoccerX.Persistence.Repositories;

namespace SoccerX.Persistence.StartUp
{
    public static class StartUpPersistence
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        public static IServiceCollection AddDependcyCollectionPersistence(this IServiceCollection services, ApplicationSettings settings)
        {
            return services
                .AddDbContext<SoccerXDbContext>(options =>
                {
                    options.UseNpgsql(settings.GetDatabaseConnectionString());
                    options.AddInterceptors(new AuditSaveChangesInterceptor());
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Tüm sorgular için tracking kapalı
                })
                .AddRepository();
        }

        #endregion

        #region Private Method

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services
                .AddScoped<IAuditLogRepository, AuditLogRepository>()
                .AddScoped<IBetSlipRepository, BetSlipRepository>()
                .AddScoped<ICityRepository, CityRepository>()
                .AddScoped<ICountryRepository, CountryRepository>()
                .AddScoped<IEmailVerificationRepository, EmailVerificationRepository>()
                .AddScoped<ILikeRepository, LikeRepository>()
                .AddScoped<INotificationRepository, NotificationRepository>()
                .AddScoped<IPaymentRepository, PaymentRepository>()
                .AddScoped<IReferralRewardRepository, ReferralRewardRepository>()
                .AddScoped<ISchedulerResultRepository, SchedulerResultRepository>()
                .AddScoped<ISubscriptionRepository, SubscriptionRepository>()
                .AddScoped<ITeamRepository, TeamRepository>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<IUserRepository, UserRepository>();
        }
        #endregion
    }
}
