using SoccerX.Application.Interfaces.FootballApiManager;

public class StandingsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Lig ID'si (API'de benzersiz) 39 (Premier Lig)
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Season { get; set; }
    /// <summary>
    /// integer Takım ID'si (belirli bir takımın sıralamasını getirir) 40 (Liverpool)
    /// </summary>
    public string? Team { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}