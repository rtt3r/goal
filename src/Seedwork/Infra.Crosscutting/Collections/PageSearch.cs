namespace Goal.Seedwork.Infra.Crosscutting.Collections;

public class PageSearch(int pageIndex, int pageSize) : IPageSearch
{
    public int PageIndex { get; private set; } = pageIndex < 0 ? 0 : pageIndex;
    public int PageSize { get; private set; } = pageSize < 1 ? int.MaxValue : pageSize;
    public string? SortBy { get; private set; }
    public SortDirection SortDirection { get; private set; } = SortDirection.Asc;

    public PageSearch()
        : this(0, int.MaxValue)
    {
    }

    public PageSearch(int pageIndex, int pageSize, string? sortBy)
        : this(pageIndex, pageSize)
    {
        SortBy = sortBy;
    }

    public PageSearch(int pageIndex, int pageSize, string? sortBy, SortDirection sortDirection)
        : this(pageIndex, pageSize, sortBy)
    {
        SortDirection = sortDirection;
    }
}
