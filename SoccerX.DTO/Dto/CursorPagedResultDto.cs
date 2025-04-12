using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerX.DTO.Dto
{
    public class CursorPagedResultDto<T, TCursor> where TCursor : IComparable
    {
        #region Field
        /// <summary>
        /// Dönen veri listesi.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// Son kaydın cursor değeri. Sonraki sayfayı getirmek için kullanılabilir.
        /// </summary>
        public TCursor? LastCursor { get; set; }

        /// <summary>
        /// Daha fazla kayıt olup olmadığını gösterir.
        /// </summary>
        public bool HasMore { get; set; }

        #endregion

        #region Constructor
        public CursorPagedResultDto() { }

        public CursorPagedResultDto(IEnumerable<T> items, TCursor? lastCursor, bool hasMore)
        {
            Items = items;
            LastCursor = lastCursor;
            HasMore = hasMore;
        }
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
