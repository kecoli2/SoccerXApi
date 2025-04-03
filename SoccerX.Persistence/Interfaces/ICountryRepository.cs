using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country?> GetByCodeAsync(string countryCode);
    }
}
