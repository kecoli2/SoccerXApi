using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TopscorersParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Ligin kimlik numarası (zorunlu)
        /// </summary>
        public int League { get; set; }
        /// <summary>
        /// integer Ligin sezon yılı – 4 haneli (YYYY) (zorunlu)
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