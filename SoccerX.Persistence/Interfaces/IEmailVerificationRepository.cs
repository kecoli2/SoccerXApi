using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces;

public interface IEmailVerificationRepository
{
    Task<Emailverifications?> GetByTokenAsync(string token);
    Task<bool> IsTokenValidAsync(string token);    
}
