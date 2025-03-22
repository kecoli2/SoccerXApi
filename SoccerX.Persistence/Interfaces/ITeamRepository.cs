using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Teams>
    {
        Task<IEnumerable<Teams>> GetTeamsByTagAsync(string tagKey, string tagValue);
    }
}
