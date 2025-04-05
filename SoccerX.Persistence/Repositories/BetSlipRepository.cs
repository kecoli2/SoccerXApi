using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Persistence.Repositories
{
    public class BetSlipRepository(SoccerXDbContext context) : GenericRepository<Betslip>(context), IBetSlipRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Betslip>> GetPremiumBetSlipsAsync() => await Context.Betslips.Where(b => b.Ispremium).ToListAsync();
        #endregion

        #region Private Method
        #endregion        
    }
}
