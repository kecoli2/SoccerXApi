using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesByCountryIdAsync(Guid countryId);
    }
}
