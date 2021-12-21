using Vantage.Infra.Crosscutting.Collections;

namespace Vantage.Infra.Http.Controllers.Results
{
    public class PagedResponse : PagedCollectionResponse<IPagedCollection>
    {
        public PagedResponse(IPagedCollection source)
            : base(source)
        {
        }
    }
}
