using System;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;

namespace SoccerX.Application.Interfaces.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task UpdateUserStatus(Guid userId, UserStatus status);
}