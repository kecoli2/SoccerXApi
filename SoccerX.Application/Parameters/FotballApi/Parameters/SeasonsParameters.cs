using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class SeasonsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Oyuncunun benzersiz ID numarası
        /// </summary>
        public int? Player { get; set; }

        public bool IsValid() => true; // TODO: Add validation logic
    }
}