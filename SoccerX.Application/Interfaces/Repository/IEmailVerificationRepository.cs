using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface IEmailVerificationRepository : IGenericRepository<Emailverification>
{
    Task<Emailverification?> GetByCodeAsync(string code);
    Task<bool> IsCodeValidAsync(string code);

}
