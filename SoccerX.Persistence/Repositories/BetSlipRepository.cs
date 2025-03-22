using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class BetSlipRepository(SoccerXDbContext context) : GenericRepository<Betslips>(context), IBetSlipRepository
    {
        public async Task<IEnumerable<Betslips>> GetPremiumBetSlipsAsync() =>
            await _context.Betslips.Where(b => b.Ispremium).ToListAsync();
    }
}
