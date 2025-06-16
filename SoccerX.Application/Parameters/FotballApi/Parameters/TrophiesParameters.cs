using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TrophiesParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Tek bir oyuncunun ID’si
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// Zorunlu ❌ string  Birden fazla oyuncu ID’si (En fazla 20 adet, "id-id-id" formatında)
        /// </summary>
        public string? Players { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer Tek bir teknik direktörün ID’si
        /// </summary>
        public int? Coach { get; set; }

        /// <summary>
        /// Zorunlu ❌ string Birden fazla teknik direktör ID’si (En fazla 20 adet, "id-id-id" formatında)
        /// </summary>
        public string? Coachs { get; set; }

        public bool IsValid()
        {
            if(Player == null && Players == null && Coach == null && Coachs == null)
            {
                throw new System.Exception("En az bir parametre sağlanmalıdır: Player, Players, Coach veya Coachs");
            }

            if(Players != null && Players.Split('-').Length > 20)
            {
                throw new System.Exception("Players parametresi en fazla 20 oyuncu ID'si içerebilir.");
            }

            if(Coachs != null && Coachs.Split('-').Length > 20)
            {
                throw new System.Exception("Coachs parametresi en fazla 20 teknik direktör ID'si içerebilir.");
            }
            return true;
        }
    }
}