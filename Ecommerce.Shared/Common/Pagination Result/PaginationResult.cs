using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Common.Pagination_Result
{
    public class PaginationResult<TEntity>
    {
        public PaginationResult(int pageIndex, int pageSize, int totalCount, IEnumerable<TEntity> data)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.totalCount = totalCount;
            Data = data;
        }

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }

        public IEnumerable<TEntity> Data { get; set; }


    }
}
