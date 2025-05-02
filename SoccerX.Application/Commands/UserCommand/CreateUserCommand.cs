using MediatR;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Commands.UserCommand
{
    public record CreateUserCommand(UserCreateDto userCreateDto) : IRequest<bool>;
}
