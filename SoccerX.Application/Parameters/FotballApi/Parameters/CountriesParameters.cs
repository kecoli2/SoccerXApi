using SoccerX.Application.Interfaces.FootballApiManager;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class CountriesParameters : IFotballApiParameters
    {
        /// <summary>
        /// The name of the country
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// [ 2 .. 6 ] characters FR, GB-ENG, IT… The Alpha code of the country
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Search Country
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