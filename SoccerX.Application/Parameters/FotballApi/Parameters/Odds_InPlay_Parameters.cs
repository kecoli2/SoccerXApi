using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class Odds_InPlay_Parameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Maça ait fixture ID’si
        /// </summary>
        public int? Fixture { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer Lig ID’si (Not: Bu endpoint’te season parametresi yerine geçer)
        /// </summary>
        public int? League { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer Bahis türü ID’si (örneğin: Maç Sonucu, Alt/Üst, Handikap)
        /// </summary>
        public int? Bet { get; set; }

        public bool IsValid() => true; // TODO: Add validation logic
    }
}