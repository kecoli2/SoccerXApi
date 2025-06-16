using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class TeamsInformationParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Takımın benzersiz kimlik numarası
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// string Takımın tam adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// integer Takımın yer aldığı ligin ID numarası
        /// </summary>
        public int? League { get; set; }

        /// <summary>
        /// integer (4 haneli YYYY formatında) İlgili sezon bilgisi (örn. 2023)
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// string Takımın bağlı olduğu ülke adı
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// string (3 karakter) Takımın kısa kodu (örn. MUFC, FCB)
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// integer Takımın saha/stadyum bilgisi ID'si
        /// </summary>
        public int? Venue { get; set; }

        /// <summary>
        /// string (minimum 3 karakter) Takım adı veya ülke adına göre arama yapmayı sağlar
        /// </summary>
        public string? Search { get; set; }

        public bool IsValid()
        {
            if (Id is null && Name is null && League is null && Season is null && Country is null && Code is null && Venue is null && Search is null)
            {
                throw new System.Exception("En az bir parametre sağlanmalıdır");
            }

            if (Search is not null && Search.Length < 3)
            {
                throw new System.Exception("Search parameter minumum 3 karakter olmalı");
            }

            if (Id is not null && Id < 0)
            {
                throw new System.Exception("Id parameter must be greater than or equal to 0");
            }

            if (Id > 0 && Search != null)
            {
                throw new System.Exception("Id parameter ile Search parameter bir arada kullanılamaz.");
            }

            return true;
        }
    }
}