using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IBetSlipRepository : IGenericRepository<Betslip>
    {
        Task<IEnumerable<Betslip>> GetPremiumBetSlipsAsync();
    }
}
