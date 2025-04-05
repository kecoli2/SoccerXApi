using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories;

public class EmailVerificationRepository(SoccerXDbContext context) : GenericRepository<Emailverification>(context), IEmailVerificationRepository
{
    #region Field
    #endregion

    #region Constructor
    #endregion

    #region Public Method
    public async Task<Emailverification?> GetByCodeAsync(string code)
    {
        return await Context.Emailverifications
            .FirstOrDefaultAsync(e => e.Code == code && e.Isused == false && e.Expiresat > DateTime.UtcNow);
    }

    public async Task<bool> IsCodeValidAsync(string code)
    {
        return await Context.Emailverifications
            .AnyAsync(e => e.Code == code && e.Isused == false && e.Expiresat > DateTime.UtcNow);
    }
    #endregion

    #region Private Method

    #endregion
}
