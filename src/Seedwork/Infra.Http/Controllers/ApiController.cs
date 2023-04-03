using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Seedwork.Infra.Http.Controllers;

public class ApiController : ControllerBase
{
    protected virtual IActionResult OkOrNotFound(object value, string message) => value is null ? NotFound(message) : Ok(value);

    protected virtual IActionResult OkOrNotFound(object value) => value is null ? NotFound() : Ok(value);

    protected virtual ActionResult<T> OkOrNotFound<T>(T value, string message) => value is null ? (ActionResult<T>)NotFound(message) : (ActionResult<T>)Ok(value);

    protected virtual ActionResult<T> OkOrNotFound<T>(T value) => value is null ? (ActionResult<T>)NotFound() : (ActionResult<T>)Ok(value);

    protected virtual ActionResult CommandFailure(ICommandResult result)
    {
        if (result.HasInputValidation())
        {
            return BadRequest(result.Notifications);
        }

        return result.HasResourceNotFound()
            ? NotFound(result.Notifications)
            : result.HasDomainViolation()
            ? UnprocessableEntity(result.Notifications)
            : result.HasExternalError() ? ServiceUnavailable(result.Notifications) : InternalServerError(result.Notifications);
    }

    protected virtual ActionResult InternalServerError(object result)
        => new InternalServerErrorObjectResult(result);

    protected virtual ActionResult InternalServerError()
        => new InternalServerErrorResult();

    protected virtual ActionResult ServiceUnavailable(object result)
        => new ServiceUnavailableObjectResult(result);

    protected virtual ActionResult ServiceUnavailable()
        => new ServiceUnavailableResult();

    protected virtual OkPagedCollectionResult Paged(IPagedCollection collection)
        => new(collection);

    protected virtual OkPagedCollectionResult<T> Paged<T>(IPagedCollection<T> collection)
        => new(collection);
}
