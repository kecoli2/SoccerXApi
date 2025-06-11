using SoccerX.Application.Interfaces.FootballApiManager;

public class OddsParameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ❌ integer Oranları alınacak maçın (fixture) ID’si
    /// </summary>
    public string? Fixture { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer İlgili ligin ID’si
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// Zorunlu ❌  integer Sezon yılı (YYYY formatında, örn: 2024)
    /// </summary>
    public string? Season { get; set; }
    /// <summary>
    /// Zorunlu ❌ string Tarih (YYYY-MM-DD formatında)
    /// </summary>
    public string? Date { get; set; }
    /// <summary>
    /// Zorunlu ❌ string Saat dilimi (Timezones endpoint’inden alınmalı)
    /// </summary>
    public string? Timezone { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Sayfa numarası (Varsayılan: 1)
    /// </summary>
    public string? Page { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Bahis sağlayıcısının ID’si
    /// </summary>
    public string? Bookmaker { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Bahis türü ID’si
    /// </summary>
    public string? Bet { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}