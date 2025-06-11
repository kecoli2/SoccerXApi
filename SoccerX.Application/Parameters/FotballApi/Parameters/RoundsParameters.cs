using SoccerX.Application.Interfaces.FootballApiManager;

public class RoundsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Ligin ID'si
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// integer (4 karakter, YYYY formatında) Ligin sezonu
    /// </summary>
    public string? Season { get; set; }
    /// <summary>
    /// boolean (Değerler: "true" veya "false") Her turun tarihlerini yanıta ekler
    /// </summary>
    public string? Current { get; set; }
    /// <summary>
    /// string Timezone endpoint'inden geçerli bir zaman dilimi
    /// </summary>
    public string? Timezone { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}