using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories
{
    public class CommentRepository(SoccerXDbContext context) : GenericRepository<Comment>(context), ICommentRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Comment>> GetCommentsByBetSlipIdAsync(Guid betSlipId) =>
            await Context.Comments.Where(c => c.Betslipid == betSlipId).ToListAsync();
        #endregion

        #region Private Method
        #endregion

    }
}
