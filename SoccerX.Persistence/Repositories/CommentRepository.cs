using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class CommentRepository(SoccerXDbContext context) : GenericRepository<Comments>(context), ICommentRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Comments>> GetCommentsByBetSlipIdAsync(Guid betSlipId) =>
            await _context.Comments.Where(c => c.Betslipid == betSlipId).ToListAsync();
        #endregion

        #region Private Method
        #endregion

    }
}
