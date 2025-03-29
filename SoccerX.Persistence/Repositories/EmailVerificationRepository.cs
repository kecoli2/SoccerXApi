using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories;

public class EmailVerificationRepository(SoccerXDbContext context) : GenericRepository<Emailverifications>(context), IEmailVerificationRepository
{
    #region Field
    #endregion

    #region Constructor
    #endregion

    #region Public Method
    public async Task<Emailverifications?> GetByTokenAsync(string token)
    {
        return await _context.Emailverifications.FirstOrDefaultAsync(e => e.Token == token && e.Isused == false && e.Expiresat > DateTime.UtcNow);
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        return await _context.Emailverifications.AnyAsync(e => e.Token == token && e.Isused == false && e.Expiresat > DateTime.UtcNow);
    }
    #endregion

    #region Private Method

    #endregion
}
