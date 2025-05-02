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
                .MapEnum<AuditAction>("auditaction", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<BetSlipStatus>("betslipstatus", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<PaymentMethod>("paymentmethod", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<PaymentStatus>("paymentstatus", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<ReferralStatus>("referralstatus", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<TransactionType>("transactiontype", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<UserRole>("userrole", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<UserStatus>("userstatus", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<UserGender>("usergender", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<AuthProvider>("authprovider", nameTranslator: new NpgSqlEnumTranslater())
                .MapEnum<SchedulerResultEnum>("schedulerresultenum", nameTranslator: new NpgSqlEnumTranslater());
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
            .MapEnum<SchedulerResultEnum>("schedulerresultenum", new NpgSqlEnumTranslater())
            .MapEnum<AuthProvider>("authprovider", new NpgSqlEnumTranslater())
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
