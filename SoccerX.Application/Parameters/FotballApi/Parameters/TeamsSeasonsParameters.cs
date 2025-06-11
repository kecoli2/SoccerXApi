using SoccerX.Application.Interfaces.FootballApiManager;

public class TeamsSeasonsParameters : IFotballApiParameters
{
    /// <summary>
    /// integer Takımın benzersiz ID numarası
    /// </summary>
    public string? Team { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}