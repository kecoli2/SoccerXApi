using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

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