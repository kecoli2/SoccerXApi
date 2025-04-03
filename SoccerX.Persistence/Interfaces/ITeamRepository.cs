using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        Task<IEnumerable<Team>> GetTeamsByTagAsync(string tagKey, string tagValue);
    }
}
