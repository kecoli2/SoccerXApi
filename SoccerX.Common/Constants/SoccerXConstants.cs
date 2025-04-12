namespace SoccerX.Common.Constants
{
    public class SoccerXConstants
    {
        #region Global
        public const string HeaderXRefreshToken = "X-Refresh-Token";
        public const string ClaimPlatform = "Platform";
        public const string RoleAdmin = "Admin";
        public const string RoleUser = "User";
        public const string RoleEditor = "Editor";
        public const string PolicySoccerX = "PolicySoccerX";
        #endregion

        #region Redis Cache Keys
        public const string RedisCountries = "RedisCountries";
        public const string RedisCountryKeyCties = "Redis_{0}_Cties";
        #endregion

        #region Memory Cache Keys
        public const string MemoryCacheJobList = "MemoryCacheJobList";
        #endregion
    }
}