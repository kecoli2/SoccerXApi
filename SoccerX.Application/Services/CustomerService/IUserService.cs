using System.Threading;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Services.CustomerService
{
    public interface IUserService
    {
        Task NewCustomerSocial(User user, CancellationToken cancellationToken);
        Task CreateUser(UserCreateDto user, CancellationToken cancellationToken);
    }
}
