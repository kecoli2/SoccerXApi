using SoccerX.Application.Interfaces.FootballApiManager;

public class MappingParameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ❌ integer Sayfa numarası (Varsayılan: 1)
    /// </summary>
    public string? Page { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}