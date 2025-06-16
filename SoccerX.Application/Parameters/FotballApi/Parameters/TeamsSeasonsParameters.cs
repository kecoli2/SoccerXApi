using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TeamsSeasonsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Takımın benzersiz ID numarası
        /// </summary>
        public int Team { get; set; }

        public bool IsValid()
        {
            if (Team <= 0)
            {
                throw new System.Exception("Team parameter must be a positive integer greater than zero.");
            }

            return true;
        }
    }
}