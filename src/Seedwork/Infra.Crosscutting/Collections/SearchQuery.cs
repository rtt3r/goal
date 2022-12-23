namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public class SearchQuery : ISearchQuery
    {
        public int PageIndex { get; private set; } = 0;
        public int PageSize { get; private set; } = int.MaxValue;
        public string SortBy { get; private set; }
        public SortDirection SortDirection { get; private set; } = SortDirection.Asc;

        public SearchQuery()
            : this(0, int.MaxValue)
        {
        }

        public SearchQuery(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex < 0 ? 0 : pageIndex;
            PageSize = pageSize < 1 ? int.MaxValue : pageSize;
        }

        public SearchQuery(int pageIndex, int pageSize, string sortBy)
            : this(pageIndex, pageSize)
        {
            SortBy = sortBy;
        }

        public SearchQuery(int pageIndex, int pageSize, string sortBy, SortDirection sortDirection)
            : this(pageIndex, pageSize, sortBy)
        {
            SortDirection = sortDirection;
        }
    }
}
