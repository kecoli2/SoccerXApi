using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class MappingParameters : IFotballApiParameters
    {
        /// <summary>
        /// Zorunlu ❌ integer Sayfa numarası (Varsayılan: 1)
        /// </summary>
        public int? Page { get; set; }

        public bool IsValid() => true; // TODO: Add validation logic
    }
}