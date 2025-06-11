using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class OddsParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Oranları alınacak maçın (fixture) ID’si
        /// </summary>
        public int? Fixture { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer İlgili ligin ID’si
        /// </summary>
        public int? League { get; set; }
        /// <summary>
        /// Zorunlu ❌  integer Sezon yılı (YYYY formatında, örn: 2024)
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// Zorunlu ❌ string Tarih (YYYY-MM-DD formatında)
        /// </summary>
        public string? Date { get; set; }

        /// <summary>
        /// Zorunlu ❌ string Saat dilimi (Timezones endpoint’inden alınmalı)
        /// </summary>
        public string? Timezone { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer Sayfa numarası (Varsayılan: 1)
        /// </summary>
        public int? Page { get; set; } = 1;

        /// <summary>
        /// Zorunlu ❌ integer Bahis sağlayıcısının ID’si
        /// </summary>
        public int? Bookmaker { get; set; }

        /// <summary>
        /// Zorunlu ❌ integer Bahis türü ID’si
        /// </summary>
        public int? Bet { get; set; }

        public bool IsValid()
        {
            if (League == null || Fixture == null || Date == null)
            {
                throw new System.Exception("League veya Fixture veya Date alanı zorunludur");
            }
            return true;
        }
    }
}