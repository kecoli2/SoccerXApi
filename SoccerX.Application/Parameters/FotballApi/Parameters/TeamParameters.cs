using SoccerX.Application.Interfaces.FootballApiManager;

public class TeamParameters : IFotballApiParameters
{
    /// <summary>
    /// 
    /// </summary>
    public string? Player { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}