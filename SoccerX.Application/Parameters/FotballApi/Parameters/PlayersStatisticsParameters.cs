using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class PlayersStatisticsParameters : IFotballApiParameters
    {
        /// <summary>
        /// Integer İstatistikleri alınacak karşılaşmanın ID’si
        /// </summary>
        public int Fixture { get; set; }

        /// <summary>
        /// Integer Belirli bir takıma ait oyuncu istatistiklerini filtrelemek için takım ID’si
        /// </summary>
        public int? Team { get; set; }

        public bool IsValid()
        {
            if (Fixture == 0)
            {
                throw new System.Exception("Fixture Boş Geçilemez");
            }
            return true;
        }
    }
}