using SoccerX.Application.Interfaces.FootballApiManager;

public class VenuesParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Stadyum ID'si (zorunlu değil) 20423 (Tüpraş Stadyumu)
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// string Stadyum adı (arama için) "Ali Sami Yen"
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// string Şehir filtresi "İstanbul"
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// string Ülke filtresi "Turkey"
    /// </summary>
    public string? Country { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}