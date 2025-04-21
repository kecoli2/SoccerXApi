using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
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

        public async Task UpdateUserStatus(Guid userId, UserStatus status)
        {
            var user = new User { Id = userId };
            Context.Users.Attach(user);
            user.Status = status;
            user.Banenddate = null;
            Context.Entry(user).Property(x=> x.Status).IsModified = true;
            Context.Entry(user).Property(x => x.Banenddate).IsModified = true;
            await SaveChangesAsync();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
