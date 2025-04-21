using MediatR;
using SoccerX.Application.Commands.Security;
using SoccerX.DTO.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SoccerX.Common.Configuration;
using System.Net.Http;
using System.Linq.Expressions;
using SoccerX.Application.Exceptions;
using SoccerX.Common.Extensions;
using System.Resources;

namespace SoccerX.Application.Handler.Security
{
    public class SocialLoginCommandHandler(IUserRepository userRepository, ITokenService tokenService,
            IHttpClientFactory httpClientFactory, ApplicationSettings applicationSettings,
            ResourceManager resourceManager)
        : IRequestHandler<SocialLoginCommand, AuthResponseDto>
    {
        #region Field

        #endregion

        #region Constructor

        #endregion

        #region Public Method
        public async Task<AuthResponseDto> Handle(SocialLoginCommand request, CancellationToken cancellationToken)
        {

            // 1) Dış sağlayıcı ID token'ını doğrula
            string externalId, email, name, surname;
            string? pictureUrl = null;
            switch (request.Provider)
            {
                case AuthProvider.Google:
                    {
                        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings());
                        externalId = payload.Subject;
                        email = payload.Email!;
                        name = payload.GivenName!;
                        surname = payload.FamilyName!;
                        pictureUrl = payload.Picture;
                        break;
                    }
                case AuthProvider.Apple:
                    {
                        var payload = await ValidateAppleIdTokenAsync(request.IdToken);
                        externalId = payload.Subject;
                        email = payload.Email;
                        name = payload.GivenName;
                        surname = payload.FamilyName;
                        break;
                    }
                default:
                    throw new NotSupportedException($"Provider {request.Provider} is not supported.");
            }

            Expression<Func<User, User>> selector = u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Username = u.Username,
                Passwordhash = u.Passwordhash,
                Banenddate = u.Banenddate,
                Status = u.Status,
                Role = u.Role
            };

            // 2) Aynı provider+externalId ile kayıtlı kullanıcı var mı?
            var existingUsers = await userRepository.FindAsync(u =>
                u.Provider == request.Provider &&
                u.Externalid == externalId, selector: selector, cancellationToken: cancellationToken);
            var user = existingUsers.FirstOrDefault();

            bool isNewUser;
            if (user == null)
            {
                // Yeni kullanıcı oluştur
                isNewUser = true;
                user = new User
                {
                    Username = email,
                    Email = email,
                    Name = name,
                    Surname = surname,
                    Provider = request.Provider,
                    Status = UserStatus.Active,
                    Role = UserRole.User,
                    Externalid = externalId,
                    Createdate = DateTime.UtcNow,
                    Isprofilecomplete = false,
                    Avatarurl = pictureUrl
                    // Gerekli diğer alanlar (Countryid, Cityid vs.) 
                };
                await userRepository.AddAsync(user);
                await userRepository.SaveChangesAsync(cancellationToken);
            }
            else
            {
                if (user.Status == UserStatus.Banned && user.Banenddate >= DateTime.Now)
                {
                    throw new UnauthorizedException("error_userBanned".FromResource(resourceManager: resourceManager, user.Banenddate?.ToString("dd/MM/yyyy HH:mm")!));
                }
                else if (user.Status == UserStatus.Banned)
                {
                    await userRepository.UpdateUserStatus(user.Id, UserStatus.Active);
                }
                // Profil tamamlanma durumuna göre işaretle
                isNewUser = !user.Isprofilecomplete;
            }

            // 3) JWT + Refresh token üret
            var authDto = tokenService.GenerateTokens(user.Id, user.Role, request.PlatformType);
            authDto.IsNewUser = isNewUser;
            authDto.Name = user.Name;
            authDto.Email = user.Email;
            authDto.SurName = user.Surname;
            return authDto;
        }
        #endregion

        #region Private Method
        private async Task<AppleIdTokenPayload> ValidateAppleIdTokenAsync(string idToken)
        {
            // Apple’ın JWKS endpoint’inden public key’leri al
            var client = httpClientFactory.CreateClient();
            var jwksJson = await client.GetStringAsync("https://appleid.apple.com/auth/keys");
            var jwks = new JsonWebKeySet(jwksJson);

            // TokenValidationParameters ayarları
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "https://appleid.apple.com",
                ValidateAudience = true,
                ValidAudience = applicationSettings.AppleClientId,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeyResolver = (_, _, kid, _) =>
                    jwks.Keys
                        .Where(k => k.Kid == kid)
                        .Select(k => (SecurityKey)k)
                        .ToList(),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var handler = new JwtSecurityTokenHandler();
            handler.MapInboundClaims = false; // Apple claim'lerini birebir almak için

            // Token'ı doğrula
            var principal = handler.ValidateToken(
                idToken,
                validationParameters,
                out var validatedToken);

            // Claim'lerden gerekli bilgileri çek
            _ = validatedToken as JwtSecurityToken
                ?? throw new SecurityTokenException("Invalid Apple ID token");

            var subject = principal.FindFirst("sub")?.Value
                             ?? throw new SecurityTokenException("sub claim missing");
            var email = principal.FindFirst(ClaimTypes.Email)?.Value
                             ?? principal.FindFirst("email")?.Value
                             ?? throw new SecurityTokenException("email claim missing");
            var givenName = principal.FindFirst("given_name")?.Value ?? string.Empty;
            var familyName = principal.FindFirst("family_name")?.Value ?? string.Empty;

            return new AppleIdTokenPayload(
                Subject: subject,
                Email: email,
                GivenName: givenName,
                FamilyName: familyName);
        }

        // Apple payload'u tutacak basit record
        private record AppleIdTokenPayload(
            string Subject,
            string Email,
            string GivenName,
            string FamilyName
        );
        #endregion
    }
}
