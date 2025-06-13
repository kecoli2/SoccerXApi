using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class StatisticsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Belirli bir oyuncunun ID’si.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// integer Zorunlu Takım ID’si. Oyuncu istatistiklerini takım özelinde filtreler.
        /// </summary>
        public int Team { get; set; }

        /// <summary>
        /// integer Lig ID’si. Sezon ve takım ile birlikte daha kesin sonuçlar alınabilir.
        /// </summary>
        public int? League { get; set; }

        /// <summary>
        /// string En az 4 karakter. Oyuncunun ismi. Lig veya Takım parametresi gerektirir.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// integer Sayfalama için kullanılır. Varsayılan değer: 1.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// integer Zorunlu Season bilgisi
        /// </summary>
        public int Season { get; set; }

        public bool IsValid()
        {
            if (Team == 0 || Page == 0 || Season == 0)
            {
                throw new System.Exception("Team, Page ve Season bilgileri zorunludur");
            }

            if (Search != null && Search.Length < 4)
            {
                throw new System.Exception("Search minumum 4 karakter olmalıdır");
            }

            return true;
        }
    }
}