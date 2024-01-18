using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Infra.Http.Controllers.Results;

public abstract class PagedCollectionResponse<T>(T source) where T : class, IPagedList
{
    public T Items { get; set; } = source;
    public long TotalCount { get; set; } = source.TotalCount;
}
