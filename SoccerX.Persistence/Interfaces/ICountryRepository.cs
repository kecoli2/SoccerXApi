using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Countries>
    {
        Task<Countries?> GetByCodeAsync(string countryCode);
    }
}
