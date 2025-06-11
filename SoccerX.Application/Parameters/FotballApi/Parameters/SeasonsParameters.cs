using SoccerX.Application.Interfaces.FootballApiManager;

public class SeasonsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Oyuncunun benzersiz ID numarası
    /// </summary>
    public string? Player { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}