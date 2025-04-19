using SoccerX.Application.Interfaces.Security;
using SoccerX.Common.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using SoccerX.Common.Constants;
using SoccerX.Common.Enums;
using SoccerX.Common.Extensions;
using SoccerX.Domain.Enums;
using SoccerX.Infrastructure.Security;

namespace SoccerX.API.Middleware;
public class TokenRefreshMiddleware
{
    #region Field
    private readonly RequestDelegate _next;
    private readonly JwtSettings _jwtSettings;
    #endregion

    #region Constructor
    public TokenRefreshMiddleware(RequestDelegate next, ApplicationSettings applicationSettings)
    {
        _next = next;
        _jwtSettings = applicationSettings.JwtSettings;
    }
    #endregion

    #region Public Method
    public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
    {
        // 1) Authorization header'dan token'ı al
        var bearer = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(bearer) && bearer.StartsWith("Bearer "))
        {
            var encryptedToken = bearer["Bearer ".Length..];

            // 2) Token'ı çöz ve doğrula
            var principal = tokenService.DecryptAndValidateToken(encryptedToken);
            if (principal == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            // 3) Expiration claim'ini oku
            var expClaim = principal.FindFirst(JwtRegisteredClaimNames.Exp);
            if (expClaim != null && long.TryParse(expClaim.Value, out var expSeconds))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
                var minutesLeft = (expirationTime - DateTimeOffset.UtcNow).TotalMinutes;

                // 4) Eşik süresinden az kaldıysa yenile
                if (minutesLeft <= _jwtSettings.RenewalThresholdMinutes)
                {
                    // Kullanıcı bilgilerini al
                    var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var roleStr = principal.FindFirst(ClaimTypes.Role)?.Value;
                    var platformStr = principal.FindFirst(SoccerXConstants.ClaimPlatform)?.Value;

                    if (Guid.TryParse(userIdStr, out var userId) && TryParseUserRole(roleStr, out var userRole) && TryParsePlatform(platformStr, out var platform))
                    {
                        // 5) Yeni tokenları üret
                        var authDto = tokenService.GenerateTokens(userId, userRole, platform);

                        // 6) Header'lara ekle
                        context.Response.Headers[SoccerXConstants.HeaderXAccessToken] = authDto.AccessToken;
                        context.Response.Headers[SoccerXConstants.HeaderXRefreshToken] = authDto.RefreshToken;
                        context.Response.Headers[SoccerXConstants.HeaderXExpiresAt] = authDto.ExpiresAt.ToString("o"); // ISO 8601
                    }
                }
            }
        }

        // Pipeline devam etsin
        await _next(context);
    }
    #endregion

    #region Private Method

    private bool TryParseUserRole(string? role, out UserRole userRole)
    {
        userRole = UserRole.User;
        return role switch
        {
            SoccerXConstants.RoleUser => Enum.TryParse<UserRole>("User", out userRole),
            SoccerXConstants.RoleEditor => Enum.TryParse<UserRole>("Editor", out userRole),
            SoccerXConstants.RoleAdmin => Enum.TryParse<UserRole>("Admin", out userRole),
            _ => false
        };
    }
    private bool TryParsePlatform(string? plat, out PlatformType platform)
    {
        // Assuming you have an extension or similar
        if (!string.IsNullOrEmpty(plat)
            && Enum.TryParse<PlatformType>(plat, out platform))
            return true;

        platform = PlatformType.Web;
        return false;
    }
    #endregion
}
