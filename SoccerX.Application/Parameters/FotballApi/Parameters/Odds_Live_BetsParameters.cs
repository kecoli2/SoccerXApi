using SoccerX.Application.Interfaces.FootballApiManager;

public class Odds_Live_BetsParameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ❌ string Bahis adının ID’si (tekil sorgu için)
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Zorunlu ❌ string (en az 3 karakter) Bahis adını aramak için kullanılır
    /// </summary>
    public string? Search { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}