using MediatR;
using SoccerX.DTO.Dto;

namespace SoccerX.Application.Commands.UserCommand
{
    public record UserCreateCommand(UserCreateDto userCreateDtoRequest) : IRequest<bool>;
}
