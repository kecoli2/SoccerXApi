using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using SoccerX.Infrastructure.Util;

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
                .MapEnum<UserRole>("userrole")
                .MapEnum<UserStatus>("userstatus")
                .MapEnum<BetSlipStatus>("betslipstatus")
                .MapEnum<AuditAction>("auditaction");
        }

        public static void NpgsqlToEnumMapRegisterAll(this NpgsqlDataSourceBuilder builder)
        {
            builder
            .MapEnum<UserRole>("userrole", new NpgSqlEnumTranslater())
            .MapEnum<UserStatus>("userstatus", new NpgSqlEnumTranslater())
            .MapEnum<AuditAction>("auditaction", new NpgSqlEnumTranslater())
            .MapEnum<BetSlipStatus>("betslipstatus", new NpgSqlEnumTranslater())
            .MapComposite<Users>()
            .MapComposite<Betslips>()
            .MapComposite<Auditlog>();
        }
        #endregion

        #region Private Methods
        #endregion

        #region Protected
        #endregion
    }
}
