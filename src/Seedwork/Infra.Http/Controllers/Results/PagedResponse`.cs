using Goal.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Http.Controllers.Results
{
    public class PagedResponse<T> : PagedCollectionResponse<IPagedCollection<T>>
    {
        public PagedResponse(IPagedCollection<T> source)
            : base(source)
        {
        }
    }
}
