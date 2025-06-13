using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class ProfilesParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Oyuncunun benzersiz ID numarası
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// string En az 3 karakterlik soyadı ile oyuncu aramak için kullanılır.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// integer Sayfalama için kullanılır. Varsayılan olarak 1. sayfa döner.
        /// </summary>
        public int Page { get; set; } = 1;

        public bool IsValid()
        {
            if (Search != null && Search.Length < 3)
            {
                throw new Exception("Search minumum 3 karakter olmalıdır");
            }
            return true;
        }
    }
}