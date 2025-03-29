using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class UserRepository(SoccerXDbContext context) : GenericRepository<Users>(context), IUserRepository
    {

        #region Constructor
        #endregion

        #region Public Method
        public async Task<Users?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<Users?> GetByUsernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        #endregion

        #region Private Method
        #endregion
    }
}
