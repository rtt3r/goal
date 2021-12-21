using Microsoft.AspNetCore.Mvc;
using Vantage.Infra.Crosscutting.Collections;
using Vantage.Infra.Http.Controllers.Results;

namespace Vantage.Infra.Http.Controllers
{
    public class OkPagedCollectionResult : OkObjectResult
    {
        public OkPagedCollectionResult(IPagedCollection value)
            : base(new PagedResponse(value))
        {
        }
    }
}
