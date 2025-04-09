using SoccerX.Common.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Common.Constants;
using SoccerX.Common.Helpers;
using SoccerX.Common.Enums;
using SoccerX.Common.Extensions;

namespace SoccerX.Infrastructure.Security;

public class JwtService: IJwtService
{
    #region Field
    private readonly JwtSettings _jwtSettings;
    #endregion

    #region Constructor
    public JwtService(ApplicationSettings applicationSettings)
    {
        _jwtSettings = applicationSettings.JwtSettings;
    }
    #endregion

    #region Public Method
    public string GenerateEncryptedToken(Guid userId, string role, PlatformType platform)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(SoccerXConstants.Claim_Platform, platform.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var rawToken = tokenHandler.WriteToken(token);

        return rawToken.Encrypt(); // senin özel EncryptionHelper ile
    }

    public ClaimsPrincipal? DecryptAndValidateToken(string encryptedToken)
    {
        try
        {
            var decrypted = encryptedToken.Decrypt(); // çözümle

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var validationParameters = new TokenValidationParameters
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

            var principal = tokenHandler.ValidateToken(decrypted, validationParameters, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }
    #endregion

    #region Private Method
    #endregion
}

