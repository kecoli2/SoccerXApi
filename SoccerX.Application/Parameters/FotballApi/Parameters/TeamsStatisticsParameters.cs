using SoccerX.Application.Interfaces.FootballApiManager;

public class TeamsStatisticsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Ligin ID'si (API'de benzersiz)
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// integer Sezon bilgisi (YYYY formatında)
    /// </summary>
    public string? Season { get; set; }
    /// <summary>
    /// integer Takımın ID'si
    /// </summary>
    public string? Team { get; set; }
    /// <summary>
    /// string İstatistiklerin hesaplanacağı son tarih YYYY-MM-DD = 2023-11-20
    /// </summary>
    public string? Date { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}