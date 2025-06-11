using SoccerX.Application.Interfaces.FootballApiManager;

public class TransfersParameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ❌ integer Transfer bilgisi istenen oyuncunun ID’si
    /// </summary>
    public string? Player { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Transfer bilgisi istenen takımın ID’si
    /// </summary>
    public string? Team { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}