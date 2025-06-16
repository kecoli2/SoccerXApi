using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TeamParameters : IFotballApiParameters
    {
        /// <summary>
        /// Oyuncu Ýd'si. Zorunlu parametre.
        /// </summary>
        public int Player { get; set; }

        public bool IsValid()
        {
            if (Player == 0)
            {
                throw new System.Exception("Player parametresi zorunludur");
            }
            return true;
        }
    }
}