using MediatR;
using System;

namespace SoccerX.Application.Commands.UserCommand
{
    public record UserVerifyEmailCommand(string code) : IRequest<bool>;
    public record UserResendVerifyEmailCodeCommand() : IRequest<bool>;
    public record UserBalaceChangeCommand(Guid userId, decimal amount) : IRequest<bool>;
}
