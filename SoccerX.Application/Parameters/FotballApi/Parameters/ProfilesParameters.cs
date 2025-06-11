using SoccerX.Application.Interfaces.FootballApiManager;

public class ProfilesParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Oyuncunun benzersiz ID numarası
    /// </summary>
    public string? Player { get; set; }
    /// <summary>
    /// string En az 3 karakterlik soyadı ile oyuncu aramak için kullanılır.
    /// </summary>
    public string? Search { get; set; }
    /// <summary>
    /// integer Sayfalama için kullanılır. Varsayılan olarak 1. sayfa döner.
    /// </summary>
    public string? Page { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}