using Newtonsoft.Json;
using SoccerX.Common.Util;
using System.Collections.Generic;


namespace SoccerX.DTO.Responses.FootballApi
{
    public class FootBallApiResponse<T>
    {
        #region Field
        public string? Get { get; set; }

        [JsonConverter(typeof(FlexibleDictionaryConverter))]
        public Dictionary<string, string>? Parameters { get; set; }

        [JsonConverter(typeof(FlexibleDictionaryConverter))]
        public Dictionary<string, string>? Errors { get; set; }
        public int Results { get; set; }
        public Paging Paging { get; set; } = new();
        public List<T>? Response { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }

    public class Paging
    {
        public int Current { get; set; }
        public int Total { get; set; }
    }
}
