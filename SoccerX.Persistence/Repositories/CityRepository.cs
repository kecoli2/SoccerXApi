using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SoccerX.Persistence.Repositories
{
    public class CityRepository(SoccerXDbContext context) : GenericRepository<Cities>(context), ICityRepository
    {
        public async Task<IEnumerable<Cities>> GetCitiesByCountryIdAsync(Guid countryId)
        {
            return await _context.Cities
                .Where(c => c.Countryid == countryId)
                .ToListAsync();
        }

        public async Task<Cities?> GetByNameAsync(string name)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
