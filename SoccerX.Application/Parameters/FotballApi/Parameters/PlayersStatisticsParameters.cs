using SoccerX.Application.Interfaces.FootballApiManager;

public class PlayersStatisticsParameters : IFotballApiParameters
{
    /// <summary>
    /// Integer İstatistikleri alınacak karşılaşmanın ID’si
    /// </summary>
    public string? Fixture { get; set; }
    /// <summary>
    /// Integer Belirli bir takıma ait oyuncu istatistiklerini filtrelemek için takım ID’si
    /// </summary>
    public string? Team { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}