using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class StandingsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Zorunlu Lig ID'si (API'de benzersiz) 39 (Premier Lig)
        /// </summary>
        public int League { get; set; }
        /// <summary>
        /// Zorunlu
        /// </summary>
        public int Season { get; set; }
        /// <summary>
        /// integer Takım ID'si (belirli bir takımın sıralamasını getirir) 40 (Liverpool)
        /// </summary>
        public int? Team { get; set; }

        public bool IsValid()
        {
            if (League == 0 || Season == 0)
            {
                throw new System.Exception("League ve Season parametreleri zorunludur");
            }

            return true;
        }
    }
}