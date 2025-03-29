using DbUp;
using DbUp.Engine;
using System.Reflection;

namespace SoccerX.Persistence.Migration
{
    public static class DatabaseMigration
    {
        #region Field

        #endregion

        #region Constructor

        #endregion

        #region Public Method
        public static void EnsureDatabaseIsUpToDate(string connectionString)
        {
            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            DatabaseUpgradeResult result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Migration failed: " + result.Error);
                Console.ResetColor();
                throw new Exception("Database migration failed", result.Error);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database migration successful!");
            Console.ResetColor();
        }

        #endregion

        #region Private Method

        #endregion

    }
}
