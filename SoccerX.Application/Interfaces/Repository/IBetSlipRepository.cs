using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface IBetSlipRepository : IGenericRepository<Betslip>
{
    Task<IEnumerable<Betslip>> GetPremiumBetSlipsAsync();
}