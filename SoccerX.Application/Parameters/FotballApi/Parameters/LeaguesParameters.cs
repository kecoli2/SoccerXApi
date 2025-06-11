using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class LeaguesParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Ligin benzersiz ID numarası.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// string Ligin adı.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// string Ligin bulunduğu ülkenin adı.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// string [2..6 karakter] Ülkenin alfa kodu (Örnek: FR, GB-ENG, IT…).
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// integer (4 karakter, YYYY formatında) Ligin sezonu.
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// integer Takımın ID numarası.
        /// </summary>
        public int? Team { get; set; }

        /// <summary>
        /// string Enum: "league" | "cup" Ligin türü (lig veya kupa).
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// string Enum: "true" | "false" Ligin aktif sezonlarını mı yoksa son sezonunu mu listeleneceğini belirtir.
        /// </summary>
        public string? Current { get; set; }

        /// <summary>
        /// string (en az 3 karakter) Lig veya ülke adına göre arama yapmayı sağlar.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// integer (en fazla 2 karakter) API'ye en son eklenen X lig/kupa listesini getirir.
        /// </summary>
        public int? Last { get; set; }

        public bool IsValid() => true; // TODO: Add validation logic
    }
}