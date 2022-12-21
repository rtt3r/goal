using System.Collections.Generic;
using System.Linq;

namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public class SearchQuery : ISearchQuery
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public IEnumerable<SortQuery> Sort { get; private set; }

        public SearchQuery()
            : this(default, int.MaxValue)
        {
        }

        public SearchQuery(int pageIndex, int pageSize, params SortQuery[] sort)
        {
            PageIndex = pageIndex < 0 ? 0 : pageIndex;
            PageSize = pageSize < 1 ? int.MaxValue : pageSize;
            Sort = sort ?? Enumerable.Empty<SortQuery>();
        }
    }
}
