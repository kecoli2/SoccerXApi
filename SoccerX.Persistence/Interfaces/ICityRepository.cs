using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ICityRepository : IGenericRepository<Cities>
    {
        Task<IEnumerable<Cities>> GetCitiesByCountryIdAsync(Guid countryId);
    }
}
