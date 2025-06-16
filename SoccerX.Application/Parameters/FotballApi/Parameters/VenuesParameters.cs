using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class VenuesParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer Stadyum ID'si (zorunlu değil) 20423 (Tüpraş Stadyumu)
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// string Stadyum adı (arama için) "Ali Sami Yen"
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// string Şehir filtresi "İstanbul"
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// string Ülke filtresi "Turkey"
        /// </summary>
        public string? Country { get; set; }

        public bool IsValid()
        {
            if (Id == null && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(Country))
            {
                throw new System.Exception("En az bir parametre sağlanmalıdır: Id, Name, City veya Country");
            }
            return true;
        }
    }
}