using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Http.Seedwork.Controllers.Results
{
    public class PagedResponse : PagedCollectionResponse<IPagedCollection>
    {
        public PagedResponse(IPagedCollection source)
            : base(source)
        {
        }
    }
}
