namespace Goal.Infra.Crosscutting.Collections;

public class PageSearch(int pageIndex, int pageSize) : IPageSearch
{
    public int PageIndex { get; private set; } = pageIndex < 0 ? 0 : pageIndex;
    public int PageSize { get; private set; } = pageSize < 1 ? int.MaxValue : pageSize;

    public PageSearch()
        : this(0, int.MaxValue)
    {
    }
}
