using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TeamsStatisticsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Ligin ID'si (API'de benzersiz)
        /// </summary>
        public int League { get; set; }

        /// <summary>
        /// integer Sezon bilgisi (YYYY formatında)
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        /// integer Takımın ID'si
        /// </summary>
        public int Team { get; set; }

        /// <summary>
        /// string İstatistiklerin hesaplanacağı son tarih YYYY-MM-DD = 2023-11-20
        /// </summary>
        public string? Date { get; set; }

        public bool IsValid()
        {
            if (League <= 0)
            {
                throw new System.Exception("League parameter must be a positive integer greater than zero.");
            }
            if (Season <= 0)
            {
                throw new System.Exception("Season parameter must be a positive integer greater than zero.");
            }
            if (Team <= 0)
            {
                throw new System.Exception("Team parameter must be a positive integer greater than zero.");
            }

            return true;
        }
    }
}