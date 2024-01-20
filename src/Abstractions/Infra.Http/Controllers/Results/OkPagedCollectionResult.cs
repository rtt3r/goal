using Goal.Infra.Crosscutting.Collections;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Infra.Http.Controllers.Results;

public class OkPagedCollectionResult(IPagedList value) : OkObjectResult(new PagedResponse(value))
{
}

public class OkPagedCollectionResult<T>(IPagedList<T> value) : OkObjectResult(new PagedResponse<T>(value))
{
}
