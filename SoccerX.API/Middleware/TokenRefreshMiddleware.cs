using Microsoft.AspNetCore.Http;
using SoccerX.Application.Interfaces.Security;
using Microsoft.Extensions.Options;
using SoccerX.Common.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using SoccerX.Application.Exceptions;
using SoccerX.Common.Constants;
using SoccerX.Common.Enums;
using SoccerX.Common.Extensions;

namespace SoccerX.API.Middleware;
public class TokenRefreshMiddleware
{
    #region Field
    private readonly RequestDelegate _next;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;
    #endregion

    #region Constructor
    public TokenRefreshMiddleware(RequestDelegate next, IJwtService jwtService, ApplicationSettings applicationSettings)
    {
        _next = next;
        _jwtService = jwtService;
        _jwtSettings = applicationSettings.JwtSettings;
    }
    #endregion

    #region Public Method
    public async Task InvokeAsync(HttpContext context)
    {
        var encryptedToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(encryptedToken))
        {
            var principal = _jwtService.DecryptAndValidateToken(encryptedToken);
            if (principal == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var expClaim = principal?.FindFirst(JwtRegisteredClaimNames.Exp);
            if (expClaim != null && long.TryParse(expClaim.Value, out var exp))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp);
                var remaining = expirationTime - DateTimeOffset.UtcNow;

                if (remaining.TotalMinutes <= _jwtSettings.RenewalThresholdMinutes)
                {
                    var userId = principal!.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = principal!.FindFirst(ClaimTypes.Role)?.Value;
                    var platform = principal!.FindFirst(SoccerXConstants.Claim_Platform)?.Value;

                    if (Guid.TryParse(userId, out var userGuid) && !string.IsNullOrEmpty(role))
                    {
                        var newToken = _jwtService.GenerateEncryptedToken(userGuid, role, platform.ToEnum(PlatformType.Web));
                        context.Response.Headers[SoccerXConstants.Header_XRefreshToken] = newToken;
                    }
                }
            }
        }

        await _next(context);
    }
    #endregion

    #region Private Method
    #endregion
}
