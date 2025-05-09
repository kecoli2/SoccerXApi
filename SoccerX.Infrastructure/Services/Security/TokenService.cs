using SoccerX.Common.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Common.Constants;
using SoccerX.Common.Helpers;
using SoccerX.Common.Enums;
using SoccerX.Domain.Enums;
using SoccerX.DTO.Responses;
using System.Security.Cryptography;

namespace SoccerX.Infrastructure.Services.Security;

public class TokenService : ITokenService
{
    #region Field
    private readonly JwtSettings _jwtSettings;
    private readonly TimeSpan _tokenLifetime;
    #endregion

    #region Constructor

    public TokenService(ApplicationSettings applicationSettings)
    {
        _jwtSettings = applicationSettings.JwtSettings;
        _tokenLifetime = TimeSpan.FromMinutes(_jwtSettings.ExpirationMinutes);
    }
    #endregion

    #region Public Method
    public AuthResponseDto GenerateTokens(Guid userId, UserRole role, PlatformType platform)
    {
        // 1) Access Token oluşturma
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jti = Guid.NewGuid().ToString();
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),            
            new Claim(ClaimTypes.Role,GetUserRole(role)),
            new Claim(SoccerXConstants.ClaimPlatform, platform.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, jti)
        };

        var expiresAt = DateTime.UtcNow.Add(_tokenLifetime);
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            SigningCredentials = creds
        };
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(descriptor);
        var rawToken = handler.WriteToken(token);
        var encryptedToken = rawToken.Encrypt();  // sizin EncryptionHelper

        // 2) Refresh Token
        var refreshToken = GenerateRefreshToken();

        return new AuthResponseDto
        {
            AccessToken = encryptedToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        };
    }

    public ClaimsPrincipal? DecryptAndValidateToken(string encryptedToken)
    {
        try
        {
            var decrypted = encryptedToken.Decrypt();

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = handler.ValidateToken(decrypted, validationParams, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    #endregion

    #region Private Method

    private string GetUserRole(UserRole role)
    {
        return role switch
        {
            UserRole.User => SoccerXConstants.RoleUser,
            UserRole.Editor => SoccerXConstants.RoleEditor,
            UserRole.Admin => SoccerXConstants.RoleAdmin,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }

    public string DecryptToken(string encryptedToken)
    {
        return encryptedToken.Decrypt();
    }
    #endregion
}

