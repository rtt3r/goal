using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Goal.Seedwork.Application.Handlers;
using Goal.Seedwork.Domain.Events;
using Goal.Seedwork.Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace Goal.Seedwork.Domain.Commands
{
    public abstract class CommandHandler
    {
        protected readonly INotificationHandler notificationHandler;
        protected readonly IBusHandler busHandler;
        protected readonly ILogger logger;

        protected CommandHandler(
            INotificationHandler notificationHandler,
            IBusHandler busHandler,
            ILogger logger)
        {
            this.notificationHandler = notificationHandler;
            this.busHandler = busHandler;
            this.logger = logger;
        }

        protected async Task RaiseEvent<TEvent>(TEvent @event)
            where TEvent : IEvent
            => await busHandler.RaiseEvent(@event);

        protected async Task NotifyContractViolations(
            ValidationResult validationResult,
            CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (ValidationFailure error in validationResult.Errors)
            {
                await notificationHandler.Handle(
                    Notification.Violation(error.ErrorCode, error.ErrorMessage, error.PropertyName),
                    cancellationToken);
            }
        }

        protected async Task NotifyDomainViolation(
            string errorCode,
            string message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await notificationHandler.Handle(
                Notification.Violation(errorCode, message),
                cancellationToken);
        }

        protected async Task NotifyFail(
            string errorCode,
            string message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await notificationHandler.Handle(
                Notification.Fail(errorCode, message),
                cancellationToken);
        }

        protected async Task NotifyInfo(
            string errorCode,
            string message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await notificationHandler.Handle(
                Notification.Fail(errorCode, message),
                cancellationToken);
        }
    }
}
