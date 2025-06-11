using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class HeadToHeadParameters : IFotballApiParameters
    {
        /// <summary>
        /// string İki takımın ID'si (tire - ile ayrılır)
        /// </summary>
        public string H2H { get; set; }

        /// <summary>
        /// integer Lig ID'si (belirli bir ligdeki karşılaşmalar için)
        /// </summary>
        public int? League { get; set; }

        /// <summary>
        /// integer Sezon bilgisi (YYYY formatında)
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// integer Son X karşılaşmayı getirir (örnek: 5)
        /// </summary>
        public int? Last { get; set; }

        /// <summary>
        /// string Belirli bir tarihteki karşılaşmalar (YYYY-MM-DD)
        /// </summary>
        public string? Date { get; set; }

        /// <summary>
        /// integer Stadyum ID'si (belirli bir sahada oynanan maçlar)
        /// </summary>
        public int? Venue { get; set; }

        /// <summary>
        /// string Maç durumu (örnek: FT, NS, PEN)
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// string Zaman dilimi (örnek: Europe/Istanbul)
        /// </summary>
        public string? Timezone { get; set; }

        public bool IsValid()
        {
            if (!string.IsNullOrWhiteSpace(H2H))
            {
                throw new System.Exception("H2H parametresi zorunludur.");

            }
            return true;
        }
    }
}