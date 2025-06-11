using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class EventsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Maçın/fikstürün benzersiz ID'si
        /// </summary>
        public int Fixture { get; set; }

        /// <summary>
        /// integer Takımın benzersiz ID'si
        /// </summary>
        public int? Team { get; set; }
        /// <summary>
        /// Oyuncunun benzersiz ID'si
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// string  Tür bilgisi (istatistik/kategori)
        /// type parametresi metin (string) formatında bir değer alır (örneğin: "goals", "passes", "cards" gibi).
        /// </summary>
        public string? Type { get; set; }

        public bool IsValid()
        {
            if (Fixture == 0)
            {
                throw new Exception("Fixture Bilgisinin girilmesi gereklidir.");
            }
            return true;
        }
    }
}