using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.DTO.Dto
{
    public class PagedResultDto<T>
    {
        #region Field
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
