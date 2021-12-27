using Microsoft.AspNetCore.Mvc;
using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Http.Controllers.Results
{
    public class OkPagedCollectionResult : OkObjectResult
    {
        public OkPagedCollectionResult(IPagedCollection value)
            : base(new PagedResponse(value))
        {
        }
    }
}
