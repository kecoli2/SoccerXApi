using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class SquadsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Takımın ID numarası
        /// </summary>
        public int? Team { get; set; }
        /// <summary>
        /// integer Oyuncunun ID numarası
        /// </summary>
        public int? Player { get; set; }

        public bool IsValid()
        {
            if(Team == null && Player == null)
            {
                throw new System.Exception("En az bir parametre girilmelidir");
            }
            return true;
        }
    }
}