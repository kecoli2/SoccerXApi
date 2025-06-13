using SoccerX.Application.Interfaces.FootballApiManager;
using System.Linq;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class SidelinedParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Tek bir oyuncunun ID’si
        /// </summary>
        public int? Player { get; set; }
        /// <summary>
        /// Zorunlu ❌ string Birden fazla oyuncu ID’si (En fazla 20 adet, "id-id-id" formatında)
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
            var isAllEmpty = Player == null && Players == null && Coach == null && Coachs == null;
            if (isAllEmpty)
            {
                throw new System.Exception("En az birtane parametre girilmelidir");
            }

            if (Players != null && Players.Split("-").Count() > 20)
            {
                throw new System.Exception("Players maksimim 20 tane ID alabilir");
            }

            if (Coachs != null && Coachs.Split("-").Count() > 20)
            {
                throw new System.Exception("Coachs maksimim 20 tane ID alabilir");
            }

            return true;
        }
    }
}