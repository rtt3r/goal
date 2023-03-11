namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public class PageSearch : IPageSearch
    {
        public int PageIndex { get; private set; } = 0;
        public int PageSize { get; private set; } = int.MaxValue;
        public string SortBy { get; private set; }
        public SortDirection SortDirection { get; private set; } = SortDirection.Asc;

        public PageSearch()
            : this(0, int.MaxValue)
        {
        }

        public PageSearch(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex < 0 ? 0 : pageIndex;
            PageSize = pageSize < 1 ? int.MaxValue : pageSize;
        }

        public PageSearch(int pageIndex, int pageSize, string sortBy)
            : this(pageIndex, pageSize)
        {
            SortBy = sortBy;
        }

        public PageSearch(int pageIndex, int pageSize, string sortBy, SortDirection sortDirection)
            : this(pageIndex, pageSize, sortBy)
        {
            SortDirection = sortDirection;
        }
    }
}
