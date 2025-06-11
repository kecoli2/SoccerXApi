using SoccerX.Application.Interfaces.FootballApiManager;
using SoccerX.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class InjuriesParameters : IFotballApiParameters
    {
        /// <summary>
        /// Integer Lig ID’si
        /// </summary>
        public int? League { get; set; }

        /// <summary>
        /// Integer (YYYY) Lig sezonu. league, team, veya player parametreleriyle birlikte kullanılması zorunludur.
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// Integer Karşılaşma ID’si
        /// </summary>
        public int? Fixture { get; set; }

        /// <summary>
        /// IntegerTakım ID’si
        /// </summary>
        public int? Team { get; set; }

        /// <summary>
        /// Integer Oyuncu ID’si
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// String (YYYY-MM-DD)  Geçerli bir tarih
        /// </summary>
        public string? Date { get; set; }

        /// <summary>
        /// String (Maks. 20 fixture ID) Bir veya birden fazla karşılaşma ID’si, "id-id-id" formatında
        /// </summary>
        public string? Ids { get; set; }

        /// <summary>
        /// String Geçerli bir saat dilimi (Timezone uç noktasından alınabilir)
        /// </summary>
        public string? Timezone { get; set; }

        public bool IsValid()
        {
            bool isAnyParameterSet =
              League.HasValue || Season.HasValue || Fixture.HasValue || Player.HasValue || Team.HasValue ||
              !string.IsNullOrWhiteSpace(Ids) || !string.IsNullOrWhiteSpace(Date) || !string.IsNullOrWhiteSpace(Timezone);

            if (!isAnyParameterSet)
            {
                throw new System.Exception("En az bir filtre parametresi girilmelidir. Lütfen geçerli bir filtre değeri sağlayın.");
            }

            return true;
        }
    }
}