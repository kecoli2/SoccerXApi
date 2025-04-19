using System;
using System.Security.Claims;
using SoccerX.Common.Enums;
using SoccerX.Domain.Enums;

namespace SoccerX.Application.Interfaces.Security;

/// <summary>
/// JWT işlemleri için servis arayüzü (şifreleme + doğrulama).
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Kullanıcı bilgileri ile şifrelenmiş JWT üretir.
    /// </summary>
    /// <param name="userId">Kullanıcının ID'si</param>
    /// <param name="role">Kullanıcının rolü</param>
    /// <param name="platform">User Type</param>
    /// <returns>Şifrelenmiş token string</returns>
    string GenerateEncryptedToken(Guid userId, UserRole role, PlatformType platform);

    /// <summary>
    /// Şifrelenmiş token'ı çözerek ClaimsPrincipal döner.
    /// </summary>
    /// <param name="encryptedToken">Şifrelenmiş JWT token</param>
    /// <returns>ClaimsPrincipal veya null</returns>
    ClaimsPrincipal? DecryptAndValidateToken(string encryptedToken);
}