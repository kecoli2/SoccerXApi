namespace SoccerX.Common.Shared.Model
{
    /// <summary>
    /// Offset-based paging model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        #region Field
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        #endregion

        #region Constructor
        public PagedResult(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
