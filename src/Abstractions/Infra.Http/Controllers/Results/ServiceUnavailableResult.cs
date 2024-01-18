using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Infra.Http.Controllers.Results;

public class ServiceUnavailableResult : StatusCodeResult
{
    public ServiceUnavailableResult()
        : base(StatusCodes.Status503ServiceUnavailable)
    {
    }
}

public class ServiceUnavailableObjectResult : ObjectResult
{
    public ServiceUnavailableObjectResult(object result)
        : base(result)
    {
        StatusCode = StatusCodes.Status503ServiceUnavailable;
    }
}
