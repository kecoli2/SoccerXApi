using SoccerX.Application.Interfaces.FootballApiManager;

public class StatisticsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Belirli bir oyuncunun ID’si.
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// integer       Takım ID’si. Oyuncu istatistiklerini takım özelinde filtreler.
    /// </summary>
    public string? Team { get; set; }
    /// <summary>
    /// integer Lig ID’si. Sezon ve takım ile birlikte daha kesin sonuçlar alınabilir.
    /// </summary>
    public string? League { get; set; }
    /// <summary>
    /// string En az 4 karakter. Oyuncunun ismi. Lig veya Takım parametresi gerektirir.
    /// </summary>
    public string? Search { get; set; }
    /// <summary>
    /// integer Sayfalama için kullanılır. Varsayılan değer: 1.
    /// </summary>
    public string? Page { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Season { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}