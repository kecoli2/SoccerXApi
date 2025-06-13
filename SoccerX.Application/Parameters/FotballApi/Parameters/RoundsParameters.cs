using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class RoundsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Ligin ID'si
        /// </summary>
        public int League { get; set; }

        /// <summary>
        /// integer (4 karakter, YYYY formatında) Ligin sezonu
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        /// boolean (Değerler: "true" veya "false") Her turun tarihlerini yanıta ekler
        /// </summary>
        public string? Current { get; set; }

        /// <summary>
        /// string Timezone endpoint'inden geçerli bir zaman dilimi
        /// </summary>
        public string? Timezone { get; set; }

        public bool IsValid()
        {
            if (League == 0 || Season == 0)
            {
                throw new System.Exception("League ve Season Bilgileri Zorunludur.");
            }
            return true;
        }
    }
}