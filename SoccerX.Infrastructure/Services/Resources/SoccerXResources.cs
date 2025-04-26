using System.Globalization;
using System.Resources;
using SoccerX.Application.Interfaces.Resources;

namespace SoccerX.Infrastructure.Services.Resources
{
    public class SoccerXResources: IResourceManager
    {
        #region Field
        private readonly ResourceManager _resourceManager;
        #endregion

        #region Constructor
        public SoccerXResources(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }
        #endregion

        #region Public Method
        public string GetString(string key)
        {
            return _resourceManager.GetString(key) ?? $"[{key}]";
        }

        public string GetString(string key, string defaultValue)
        {
            return _resourceManager.GetString(key) ?? defaultValue;
        }

        public string GetString(string key, params object?[] args)
        {
            return string.Format(GetString(key), args);
        }

        public string GetCultureKey()
        {
            return CultureInfo.CurrentCulture.Name;
        }

        #endregion

        #region Private Method
        #endregion
    }
}
