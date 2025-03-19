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
                .MapEnum<UserStatus>("userstatus");
        }

        public static void NpgsqlToEnumMapRegisterAll(this NpgsqlDataSourceBuilder builder)
        {
            builder
            .MapEnum<UserRole>("userrole", new NpgSqlEnumTranslater())
            .MapEnum<UserStatus>("userstatus", new NpgSqlEnumTranslater())
            .MapComposite<Users>();
        }
        #endregion

        #region Private Methods
        #endregion

        #region Protected
        #endregion
    }
}
