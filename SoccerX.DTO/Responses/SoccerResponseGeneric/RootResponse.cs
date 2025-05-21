using System.Collections.Generic;

namespace SoccerX.DTO.Responses.SoccerResponseGeneric
{
    public class RootResponse<T>
    {
        #region Field
        public string? Get { get; set; }
        public Dictionary<string, string>? Parameters { get; set; }
        public List<string>? Errors { get; set; }
        public int Results { get; set; }
        public Paging Paging { get; set; }
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
