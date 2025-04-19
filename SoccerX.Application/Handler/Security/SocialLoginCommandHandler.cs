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
using SoccerX.Application.Services.CustomerService;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SoccerX.Common.Configuration;
using System.Net.Http;

namespace SoccerX.Application.Handler.Security
{
    public class SocialLoginCommandHandler : IRequestHandler<SocialLoginCommand, AuthResponseDto>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationSettings _applicationSettings;
        #endregion

        #region Constructor
        public SocialLoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IHttpClientFactory httpClientFactory, ApplicationSettings applicationSettings)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _httpClientFactory = httpClientFactory;
            _applicationSettings = applicationSettings;
        }
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

            // 2) Aynı provider+externalId ile kayıtlı kullanıcı var mı?
            var existingUsers = await _userRepository.FindAsync(u =>
                u.Provider == request.Provider &&
                u.Externalid == externalId, cancellationToken: cancellationToken);
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
                    Externalid = externalId,
                    Createdate = DateTime.UtcNow,
                    Isprofilecomplete = false,
                    Avatarurl = pictureUrl
                    // Gerekli diğer alanlar (Countryid, Cityid vs.) 
                };
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // Profil tamamlanma durumuna göre işaretle
                isNewUser = !user.Isprofilecomplete;
            }

            // 3) JWT + Refresh token üret
            var authDto = _tokenService.GenerateTokens(user.Id, user.Role, request.PlatformType);
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
            var client = _httpClientFactory.CreateClient();
            var jwksJson = await client.GetStringAsync("https://appleid.apple.com/auth/keys");
            var jwks = new JsonWebKeySet(jwksJson);

            // TokenValidationParameters ayarları
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "https://appleid.apple.com",
                ValidateAudience = true,
                ValidAudience = _applicationSettings.AppleClientId,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
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
            var jwt = validatedToken as JwtSecurityToken
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
