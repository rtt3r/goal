using Goal.Infra.Crosscutting.Collections;
using Microsoft.AspNetCore.Mvc;

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
