using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class CoachsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer      
        /// Antrenörün benzersiz ID’si
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// integer      
        /// Antrenörün görev yaptığı takımın ID’si
        /// </summary>
        public int? Team { get; set; }

        /// <summary>
        /// Antrenörün adıyla arama yapılır            
        /// En az 3 karakter olmalı
        /// </summary>
        public string? Search { get; set; }

        public bool IsValid()
        {
            if (Search != null && Search.Length < 3)
            {
                throw new Exception("Minumum 3 karakter giriniz");
            }

            if (Search == null && Team == null && Id == null)
            {
                throw new Exception("En az bir parametre girilmelidir");
            }
            return true;
        }
    }
}