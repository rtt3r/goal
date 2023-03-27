using System.Collections.Generic;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Seedwork.Infra.Http.Controllers
{
    public class ApiController : ControllerBase
    {
        protected virtual IActionResult OkOrNotFound(object value, string message)
        {
            if (value is null)
            {
                return NotFound(message);
            }

            return Ok(value);
        }

        protected virtual IActionResult OkOrNotFound(object value)
        {
            if (value is null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        protected virtual ActionResult<T> OkOrNotFound<T>(T value, string message)
        {
            if (value is null)
            {
                return NotFound(message);
            }

            return Ok(value);
        }

        protected virtual ActionResult<T> OkOrNotFound<T>(T value)
        {
            if (value is null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        protected virtual ActionResult CommandFailure(ICommandResult result)
        {
            if (result.HasInputValidation())
            {
                return BadRequest(result.Notifications);
            }

            if (result.HasResourceNotFound())
            {
                return NotFound(result.Notifications);
            }

            if (result.HasDomainViolation())
            {
                return UnprocessableEntity(result.Notifications);
            }

            if (result.HasExternalError())
            {
                return ServiceUnavailable(result.Notifications);
            }

            return InternalServerError(result.Notifications);
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
    }
}
