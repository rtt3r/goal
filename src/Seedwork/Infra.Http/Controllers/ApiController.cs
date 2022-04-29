using Goal.Infra.Crosscutting.Collections;
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

        protected virtual ActionResult InternalServerError(object result) => new InternalServerErrorObjectResult(result);

        protected virtual OkPagedCollectionResult Paged(IPagedCollection collection) => new(collection);
    }
}
