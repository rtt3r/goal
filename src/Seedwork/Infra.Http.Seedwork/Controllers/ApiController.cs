using System.Collections.Generic;
using System.Threading.Tasks;
using Goal.Domain.Bus;
using Goal.Domain.Notifications;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Infra.Http.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly INotificationHandler notificationHandler;
        private readonly IBusHandler busHandler;

        protected ApiController(
            INotificationHandler notificationHandler,
            IBusHandler busHandler)
            : this()
        {
            this.notificationHandler = notificationHandler;
            this.busHandler = busHandler;
        }

        protected ApiController()
            : base()
        {
        }

        protected IEnumerable<Notification> Notifications => notificationHandler.GetNotifications();

        protected bool IsValidOperation() => (!notificationHandler.HasNotifications());

        protected async Task NotifyError(string code, string message) => await busHandler.RaiseEvent(new Notification(code, message));

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
