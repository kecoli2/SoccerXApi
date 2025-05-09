using System.Security.Claims;

namespace SoccerX.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public static string? GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string? GetUserRole(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Role)?.Value;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
