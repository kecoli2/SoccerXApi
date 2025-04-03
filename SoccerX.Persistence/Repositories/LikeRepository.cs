using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class LikeRepository(SoccerXDbContext context) : GenericRepository<Like>(context), ILikeRepository
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<int> CountLikesByBetSlipIdAsync(Guid betSlipId) => await _context.Likes.CountAsync(l => l.Betslipid == betSlipId);
        #endregion

        #region Private Method
        #endregion                    
    }
}    