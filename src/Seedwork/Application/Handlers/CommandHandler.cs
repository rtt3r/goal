using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Goal.Seedwork.Application.Handlers;
using Goal.Seedwork.Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace Goal.Seedwork.Domain.Commands
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly INotificationHandler notificationHandler;
        private readonly ILogger logger;

        protected CommandHandler(
            IUnitOfWork unitOfWork,
            INotificationHandler notificationHandler,
            ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.notificationHandler = notificationHandler;
            this.logger = logger;
        }

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
