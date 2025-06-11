using SoccerX.Application.Interfaces.FootballApiManager;

public class TopscorersParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Ligin kimlik numarası (zorunlu)
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// integer Ligin sezon yılı – 4 haneli (YYYY) (zorunlu)
    /// </summary>
    public string? Season { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}