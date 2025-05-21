namespace SoccerX.Common.Constants
{
    public class SoccerXConstants
    {
        #region Global
        public const string HeaderXAccessToken = "HeaderXAccessToken";
        public const string HeaderXRefreshToken = "X-Refresh-Token";
        public const string HeaderXExpiresAt = "HeaderXExpiresAt";
        public const string HeaderXPlatForm = "HeaderXPlatForm";
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

        #region Controller Action Key
        
        #region UserController
        public const string User_Register = "/api/user/register";
        public const string User_Register_Admin = "/api/user/registerAdmin";
        public const string User_VerifyEmail = "/api/user/verifyemail";
        public const string User_SendNewVerifyEmaill = "/api/user/SendNewVerifyEmail";
        #endregion

        #region AuthController
        public const string Authenticate_Local = "/api/auth/local-login";
        public const string Authenticate_Social = "/api/auth/social-login";
        #endregion

        #region FootBallApiConst
        public const string RapidapiHost = "x-rapidapi-host";
        public const string RapidapiKey = "x-rapidapi-key";
        public const string RapidApisportsKey= "x-apisports-key";
        #endregion
        #endregion
    }
}