namespace SoccerX.Common.Shared.Model
{
    /// <summary>
    /// Cursor-based paging model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCursor"></typeparam>
    public class CursorPagedResult<T,TCursor> where TCursor: IComparable
    {
        #region Field
        /// <summary>
        /// Getirilen veri listesi.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Son kaydın cursor değeri; bundan sonraki sayfa için kullanılacak.
        /// </summary>
        public TCursor? LastCursor { get; set; }
        #endregion

        #region Constructor
        public CursorPagedResult(IEnumerable<T> items, TCursor? lastCursor)
        {
            Items = items;
            LastCursor = lastCursor;
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
