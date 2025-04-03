using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country?> GetByCodeAsync(string countryCode);
}