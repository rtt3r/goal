using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Http.Controllers.Results
{
    public class PagedResponse : PagedCollectionResponse<IPagedCollection>
    {
        public PagedResponse(IPagedCollection source)
            : base(source)
        {
        }
    }
}