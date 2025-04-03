using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface ITeamRepository : IGenericRepository<Team>
{
    Task<IEnumerable<Team>> GetTeamsByTagAsync(string tagKey, string tagValue);
}