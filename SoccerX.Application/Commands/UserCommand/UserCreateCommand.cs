using MediatR;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Commands.UserCommand
{
    public record UserCreateCommand(UserCreateDto userCreateDtoRequest) : IRequest<bool>;
}
