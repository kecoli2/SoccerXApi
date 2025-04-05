using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories
{
    public class UserRepository(SoccerXDbContext context) : GenericRepository<User>(context), IUserRepository
    {

        #region Constructor
        #endregion

        #region Public Method
        public async Task<User?> GetByEmailAsync(string email) =>
            await Context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByUsernameAsync(string username) =>
            await Context.Users.FirstOrDefaultAsync(u => u.Username == username);
        #endregion

        #region Private Method
        #endregion
    }
}
