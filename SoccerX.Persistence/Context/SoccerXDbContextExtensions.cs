using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SoccerX.Application.Exceptions;
using SoccerX.Common.Properties;

namespace SoccerX.Persistence.Context
{
    public partial class SoccerXDbContext
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    var fieldName = ExtractFieldNameFromException(pgEx, out var entityName, out var propertyName);
                    Console.WriteLine($"Unique violation on field: {fieldName}");
                    var dic = new Dictionary<string, string[]>();
                    dic.TryAdd(entityName, new[] { propertyName });
                    var resourceManager = string.Format(new ResourceManager(typeof(Resources))?.GetString("error_database_tableconstraint") ?? "Table_Constraint : {0}", entityName);
                    throw new DatabaseConstraintException(dic, resourceManager);
                }
                throw;
            }
        }
        
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    var fieldName = ExtractFieldNameFromException(pgEx, out var entityName, out var propertyName);
                    Console.WriteLine($"Unique violation on field: {fieldName}");
                    var dic = new Dictionary<string, string[]>();
                    dic.TryAdd(entityName, new[] { propertyName });
                    var resourceManager = string.Format(new ResourceManager(typeof(Resources))?.GetString("error_database_tableconstraint") ?? "Table_Constraint : {0}", entityName);
                    throw new DatabaseConstraintException(dic, resourceManager);
                }
                throw;
            }
        }


        #endregion

        #region Private Method
        private string ExtractFieldNameFromException(PostgresException pgEx, out string entityName, out string propertyName)
        {
            entityName = "UnKnow Entity";
            propertyName = "UnKnow Field";
            if (!string.IsNullOrEmpty(pgEx.ConstraintName))
            {
                var constraintParts = pgEx.ConstraintName.Split('_');
                if (constraintParts.Length < 3) return pgEx.ConstraintName; // uq_table_column formatı
                propertyName = constraintParts[2]; // column name
                entityName = constraintParts[1]; // entity name
                return pgEx.ConstraintName;
            }

            // 2. Fallback: Hata mesajından çıkarsama
            var message = pgEx.Message.ToLower();
            if (message.Contains("email")) return "Email";
            if (message.Contains("username")) return "Username";
            if (message.Contains("phone")) return "Phone";

            // 3. Son çare olarak constraint adını döndür
            return pgEx.ConstraintName ?? "UnknownField";
        }
    }
    #endregion
}
