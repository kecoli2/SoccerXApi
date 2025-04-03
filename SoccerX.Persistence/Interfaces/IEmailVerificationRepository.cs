using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces;

public interface IEmailVerificationRepository
{
    Task<Emailverification?> GetByCodeAsync(string code);
    Task<bool> IsCodeValidAsync(string code);

}
