using Goal.Seedwork.Infra.Crosscutting.Collections;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Seedwork.Infra.Http.Controllers.Results
{
    public class OkPagedCollectionResult : OkObjectResult
    {
        public OkPagedCollectionResult(IPagedCollection value)
            : base(new PagedResponse(value))
        {
        }
    }

    public class OkPagedCollectionResult<T> : OkObjectResult
    {
        public OkPagedCollectionResult(IPagedCollection<T> value)
            : base(new PagedResponse<T>(value))
        {
        }
    }
}
