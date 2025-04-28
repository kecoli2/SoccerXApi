using Quartz.Logging;

namespace SoccerX.Infrastructure.Jobs.Base
{
    public class ConsoleLogProviderQuartz: ILogProvider
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine($"[{level}] {func()}", parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Method
        #endregion
    }
}
