using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TopAssistsParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ✅ integer Ligin veya kupanın ID’si
        /// </summary>
        public int League { get; set; }

        /// <summary>
        /// Zorunlu ✅ integer (YYYY formatında 4 hane) İlgili sezon yılı (örneğin: 2024)
        /// </summary>
        public int Season { get; set; }

        public bool IsValid()
        {
            if (League <= 0)
            {
                throw new System.Exception("League parameter must be a positive integer greater than zero.");
            }

            if (Season <= 0 || Season.ToString().Length != 4)
            {
                throw new System.Exception("Season parameter must be a valid year in YYYY format.");
            }

            return true;
        }
    }
}