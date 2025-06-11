using SoccerX.Application.Interfaces.FootballApiManager;

public class SquadsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer       Takımın kimlik numarası
    /// </summary>
    public string? Team { get; set; }
    /// <summary>
    /// integer Oyuncunun kimlik numarası
    /// </summary>
    public string? Player { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}