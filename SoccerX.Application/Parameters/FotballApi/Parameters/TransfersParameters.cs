using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TransfersParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Transfer bilgisi istenen oyuncunun ID’si
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer Transfer bilgisi istenen takımın ID’si
        /// </summary>
        public int? Team { get; set; }

        public bool IsValid()
        {
            if (Player == null && Team == null)
            {
                throw new System.Exception("En az bir parametre sağlanmalıdır: Player veya Team");
            }
            return true;
        }
    }
}