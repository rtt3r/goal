using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Seedwork.Infra.Http.Controllers.Results;

public class InternalServerErrorResult : StatusCodeResult
{
    public InternalServerErrorResult()
        : base(StatusCodes.Status500InternalServerError)
    {
    }
}

public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object error)
        : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
