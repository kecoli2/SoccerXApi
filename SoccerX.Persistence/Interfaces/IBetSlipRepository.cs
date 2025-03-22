using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IBetSlipRepository : IGenericRepository<Betslips>
    {
        Task<IEnumerable<Betslips>> GetPremiumBetSlipsAsync();
    }
}
