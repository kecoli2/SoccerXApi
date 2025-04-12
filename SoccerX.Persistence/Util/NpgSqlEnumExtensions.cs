using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;

namespace SoccerX.Persistence.Util
{
    public static class NpgSqlEnumExtensions
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static void NpgsqlToEnumMapRegisterAll(this NpgsqlDbContextOptionsBuilder builder)
        {
            builder
                .MapEnum<AuditAction>("auditaction")
                .MapEnum<BetSlipStatus>("betslipstatus")
                .MapEnum<PaymentMethod>("paymentmethod")
                .MapEnum<PaymentStatus>("paymentstatus")
                .MapEnum<ReferralStatus>("referralstatus")
                .MapEnum<TransactionType>("transactiontype")
                .MapEnum<UserRole>("userrole")
                .MapEnum<UserStatus>("userstatus")
                .MapEnum<UserGender>("usergender")
                .MapEnum<SchedulerResultEnum>("scheduler_result_enum");
        }

        public static void NpgsqlToEnumMapRegisterAll(this NpgsqlDataSourceBuilder builder)
        {
            builder
            .MapEnum<AuditAction>("auditaction", new NpgSqlEnumTranslater())
            .MapEnum<BetSlipStatus>("betslipstatus", new NpgSqlEnumTranslater())
            .MapEnum<PaymentMethod>("paymentmethod", new NpgSqlEnumTranslater())
            .MapEnum<PaymentStatus>("paymentstatus", new NpgSqlEnumTranslater())
            .MapEnum<ReferralStatus>("referralstatus", new NpgSqlEnumTranslater())
            .MapEnum<TransactionType>("transactiontype", new NpgSqlEnumTranslater())
            .MapEnum<UserRole>("userrole", new NpgSqlEnumTranslater())
            .MapEnum<UserStatus>("userstatus", new NpgSqlEnumTranslater())
            .MapEnum<UserGender>("usergender", new NpgSqlEnumTranslater())
            .MapEnum<SchedulerResultEnum>("scheduler_result_enum", new NpgSqlEnumTranslater())
            .MapComposite<Auditlog>()
            .MapComposite<Betslip>()
            .MapComposite<Payment>()
            .MapComposite<Referralreward>()
            .MapComposite<Transaction>()
            .MapComposite<User>()
            .MapComposite<Schedulerresult>();
        }
        #endregion

        #region Private Methods
        #endregion

        #region Protected
        #endregion
    }
}
