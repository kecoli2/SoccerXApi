using SoccerX.Application.Interfaces.FootballApiManager;

public class TopRedCardsParameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ✅ integer Ligin veya kupanın ID’si
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// Zorunlu ✅ integer (YYYY formatında 4 hane) İlgili sezon yılı (örnek: 2024)
    /// </summary>
    public string? Season { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}