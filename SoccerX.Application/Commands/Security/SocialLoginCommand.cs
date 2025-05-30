﻿using MediatR;
using SoccerX.Common.Enums;
using SoccerX.Domain.Enums;
using SoccerX.DTO.Responses;

namespace SoccerX.Application.Commands.Security
{
    public record SocialLoginCommand(string IdToken, AuthProvider Provider, PlatformType PlatformType) : IRequest<AuthResponseDto>;
}
