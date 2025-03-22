using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByUsernameAsync(string username);
    }
}
