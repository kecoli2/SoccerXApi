using SoccerX.Application.Interfaces.FootballApiManager;

public class SidelinedParameters : IFotballApiParameters
{
    /// <summary>
    /// Zorunlu ❌ integer Tek bir oyuncunun ID’si
    /// </summary>
    public string? Player { get; set; }
    /// <summary>
    /// Zorunlu ❌ string Birden fazla oyuncu ID’si (En fazla 20 adet, "id-id-id" formatında)
    /// </summary>
    public string? Players { get; set; }
    /// <summary>
    /// Zorunlu ❌ integer Tek bir teknik direktörün ID’si
    /// </summary>
    public string? Coach { get; set; }
    /// <summary>
    /// Zorunlu ❌ string Birden fazla teknik direktör ID’si (En fazla 20 adet, "id-id-id" formatında)
    /// </summary>
    public string? Coachs { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}