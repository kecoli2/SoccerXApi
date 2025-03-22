using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class LikeRepository(SoccerXDbContext context) : GenericRepository<Likes>(context), ILikeRepository
    {
        public async Task<int> CountLikesByBetSlipIdAsync(Guid betSlipId) =>
            await _context.Likes.CountAsync(l => l.Betslipid == betSlipId);
    }
}
