using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Http.Seedwork.Controllers.Results
{
    public class PagedResponse<T> : PagedCollectionResponse<IPagedCollection<T>>
    {
        public PagedResponse(IPagedCollection<T> source)
            : base(source)
        {
        }
    }
}
