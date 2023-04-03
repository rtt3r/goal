using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Http.Controllers.Results;

public class PagedResponse : PagedCollectionResponse<IPagedCollection>
{
    public PagedResponse(IPagedCollection source)
        : base(source)
    {
    }
}

public class PagedResponse<T> : PagedCollectionResponse<IPagedCollection<T>>
{
    public PagedResponse(IPagedCollection<T> source)
        : base(source)
    {
    }
}
