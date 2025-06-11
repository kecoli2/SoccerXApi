using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class BetsParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ int Belirli bir bahis türünün ID’si
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Zorunlu ❌ string (min. 3 karakter) Bahis adı ile arama yapmak için
        /// </summary>
        public string? Search { get; set; }

        // TODO: Add validation logic
        public bool IsValid()
        {
            if (Search != null && Search.Length < 3) {
                throw new Exception("Minumum 3 karakter giriniz");
            }
            return true;
        }  
    }
}