using SoccerX.Application.Interfaces.FootballApiManager;

public class PredictionsParameters : IFotballApiParameters
{
    /// <summary>
    /// Integer  Tahmini alınacak karşılaşmanın ID’si
    /// </summary>
    public string? Fixture { get; set; }

    public bool IsValid() => true; // TODO: Add validation logic
}