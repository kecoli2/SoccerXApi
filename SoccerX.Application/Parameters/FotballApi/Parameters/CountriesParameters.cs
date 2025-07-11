using Newtonsoft.Json;
using SoccerX.Application.Interfaces.FootballApiManager;
using SoccerX.Common.Attributes;
using System;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class CountriesParameters : IFotballApiParameters
    {
        /// <summary>
        /// The name of the country
        /// </summary>        
        [QueryName("name")]
        [TransformTurkishCharacters]
        public string? Name { get; set; }
        /// <summary>
        /// [ 2 .. 6 ] characters FR, GB-ENG, IT… The Alpha code of the country
        /// </summary>

        [QueryName("code")]
        [TransformTurkishCharacters]
        public string? Code { get; set; }
        /// <summary>
        /// Search Country
        /// </summary>
        [QueryName("search")]
        [TransformTurkishCharacters]
        public string? Search { get; set; }

        public bool IsValid()
        {
            TransformTurkishCharactersAttribute.ApplyTransformations(this);
            if (Search != null && Search.Length < 3)
            {
                throw new Exception("Minumum 3 karakter giriniz");
            }
            return true;
        }
    }
}