using MediatR;
using SoccerX.Common.Enums;
using SoccerX.DTO.Responses;

namespace SoccerX.Application.Commands.Security
{
    public record LocalLoginCommand(string EmailUserName, string Password, PlatformType Platform) : IRequest<AuthResponseDto>;
}
