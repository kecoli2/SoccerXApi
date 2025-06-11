using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class Odds_Live_BetsParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ string Bahis adının ID’si (tekil sorgu için)
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Zorunlu ❌ string (en az 3 karakter) Bahis adını aramak için kullanılır
        /// </summary>
        public string? Search { get; set; }

        public bool IsValid()
        {
            if (Search != null && Search.Length < 3)
            {
                throw new Exception("Minumum 3 karakter girilmelidir.");
            }
            return true;
        }
    }
}