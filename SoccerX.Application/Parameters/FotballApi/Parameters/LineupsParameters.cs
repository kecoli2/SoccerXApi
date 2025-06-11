using SoccerX.Application.Interfaces.FootballApiManager;

namespace SoccerX.Application.Parameters.FotballApi.Parameters
{
    public class LineupsParameters : IFotballApiParameters
    {
        /// <summary>
        /// integer İstenen maçın benzersiz ID'si. Örn: ?fixture=12345
        /// </summary>
        public int Fixture { get; set; }
        /// <summary>
        /// integer Filtreleme için takım ID'si. Örn: &team=541 (Real Madrid)
        /// </summary>
        public int? Team { get; set; }
        /// <summary>
        /// integer Belirli bir oyuncunun kadroda olup olmadığını kontrol için ID. Örn: &player=276 (Courtois)
        /// </summary>
        public int? Player { get; set; }
        /// <summary>
        /// string Özel veri türü filtresi. Örn: &type=formation (sadece diziliş bilgisi)
        /// </summary>
        public string? Type { get; set; }

        public bool IsValid()
        {
            if (Fixture == 0)
            {
                throw new System.Exception("Fixture parameter is required and must be a valid match ID.");
            }
            return true;
        }
    }
}