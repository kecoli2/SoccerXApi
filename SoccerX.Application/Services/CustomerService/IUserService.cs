using System;
using System.Threading;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Services.CustomerService
{
    public interface IUserService
    {
        Task<Guid> GetActiveUserId();
        Task NewCustomerSocial(User user, CancellationToken cancellationToken);
        Task CreateUser(UserCreateDto user, CancellationToken cancellationToken);
        Task VerifiedUser(string code, CancellationToken cancellationToken);
        Task SendRenewVerificationCode(CancellationToken cancellationToken);
        Task<bool> ChangeCurrentPassword(Guid securityKey, string oldPassword, string newPassword, CancellationToken cancellationToken);

    }
}
