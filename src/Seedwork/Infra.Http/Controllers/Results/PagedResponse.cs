using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Http.Controllers.Results;

public class PagedResponse(IPagedList source) : PagedCollectionResponse<IPagedList>(source)
{
}

public class PagedResponse<T>(IPagedList<T> source) : PagedCollectionResponse<IPagedList<T>>(source)
{
}
