using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class BookmakersParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Belirli bir bahis sağlayıcının ID’si
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Zorunlu ❌ string (min. 3 karakter) Sağlayıcı adına göre arama yapmak için
        /// </summary>
        public string? Search { get; set; }

        public bool IsValid()
        {
            if (Search != null && Search.Length < 3)
            {
                throw new Exception("Minumum 3 karakter giriniz");
            }
            return true;
        }
    }
}