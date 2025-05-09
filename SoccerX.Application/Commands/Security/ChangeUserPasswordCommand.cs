using MediatR;
using System;

namespace SoccerX.Application.Commands.Security
{
    public record class ChangeUserPasswordCommand(string oldPassword, string newPassword, Guid securityKey) : IRequest<bool>;
}