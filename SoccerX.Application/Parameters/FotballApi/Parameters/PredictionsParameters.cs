using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class PredictionsParameters : IFotballApiParameters
    {
        /// <summary>
        /// Integer  Tahmini alınacak karşılaşmanın ID’si
        /// </summary>
        public int Fixture { get; set; }

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