using Microsoft.AspNetCore.Mvc;
using Vantage.Infra.Crosscutting.Collections;

namespace Vantage.Infra.Http.Controllers.Results
{
    public class OkPagedCollectionResult : OkObjectResult
    {
        public OkPagedCollectionResult(IPagedCollection value)
            : base(new PagedResponse(value))
        {
        }
    }
}
