using SoccerX.Application.Interfaces.FootballApiManager;

public class Odds_InPlay_Parameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ❌ integer Maça ait fixture ID’si
    /// </summary>
    public string? Fixture { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Lig ID’si (Not: Bu endpoint’te season parametresi yerine geçer)
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Bahis türü ID’si (örneğin: Maç Sonucu, Alt/Üst, Handikap)
    /// </summary>
    public string? Bet { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}