using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class FixturesParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Maçın benzersiz ID'si "id" (örnek: 12345)
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// string En fazla 20 maç ID'si (birden fazla ID için tire - ile ayrılır) "id-id-id" (örnek: 123-456-789)
        /// </summary>
        public string? Ids { get; set; }

        /// <summary>
        /// Tüm canlı maçlar veya belirli liglerdeki canlı maçlar "all" veya "ligID1-ligID2"
        /// </summary>
        public string? Live { get; set; }

        /// <summary>
        /// Belirli bir tarihteki maçlar YYYY-MM-DD (örnek: 2023-10-15)
        /// </summary>
        public string? Date { get; set; }

        /// <summary>
        /// integer Lig ID'si Geçerli bir lig ID'si
        /// </summary>
        public int? League { get; set; }

        /// <summary>
        /// integer Lig sezonu 4 haneli yıl (YYYY) (örnek: 2023)
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// integer Takım ID'si Geçerli bir takım ID'si
        /// </summary>
        public int? Team { get; set; }

        /// <summary>
        /// integer (≤ 2 karakter) Takımın son X maçı 1 ile 99 arasında değer
        /// </summary>
        public int? Last { get; set; }

        /// <summary>
        /// integer (≤ 2 karakter) Takımın sonraki X maçı 1 ile 99 arasında değer
        /// </summary>
        public int? Next { get; set; }

        /// <summary>
        /// string Başlangıç tarihi (tarih aralığı) YYYY-MM-DD
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        /// string Bitiş tarihi (tarih aralığı) YYYY-MM-DD
        /// </summary>
        public string? To { get; set; }

        /// <summary>
        /// string Maçın tur bilgisi Lig/kupa tur adı (örnek: "Group Stage")
        /// </summary>
        public string? Round { get; set; }

        /// <summary>
        /// string Maçın durumu (kısa kod) "NS", "FT", "NS-PST-FT" (birden fazla durum için tire - ile ayrılır)
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// integer Stadyum ID'si Geçerli bir stadyum ID'si
        /// </summary>
        public int? Venue { get; set; }

        /// <summary>
        /// string Zaman dilimi Timezone endpoint'inden geçerli bir zaman dilimi (örnek: "Europe/Istanbul")
        /// </summary>
        public string? Timezone { get; set; }

        public bool IsValid()
        {
            bool isAnyParameterSet =
                Id.HasValue ||
                !string.IsNullOrWhiteSpace(Ids) ||
                !string.IsNullOrWhiteSpace(Live) ||
                !string.IsNullOrWhiteSpace(Date) ||
                League.HasValue ||
                Season.HasValue ||
                Team.HasValue ||
                Last.HasValue ||
                Next.HasValue ||
                !string.IsNullOrWhiteSpace(From) ||
                !string.IsNullOrWhiteSpace(To) ||
                !string.IsNullOrWhiteSpace(Round) ||
                !string.IsNullOrWhiteSpace(Status) ||
                Venue.HasValue ||
                !string.IsNullOrWhiteSpace(Timezone);

            if (!isAnyParameterSet)
            {
                throw new System.Exception("En az bir filtre parametresi girilmelidir. Lütfen geçerli bir filtre değeri sağlayın.");
            }
            return isAnyParameterSet;
        }
    }
}
