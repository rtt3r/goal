using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Goal.Domain.Seedwork;
using Goal.Domain.Seedwork.Commands;
using Goal.Domain.Seedwork.Notifications;

namespace Goal.Application.Seedwork.Handlers
{
    public abstract class CommandHandler
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IBusHandler busHandler;
        protected readonly INotificationHandler notificationHandler;

        public CommandHandler(
            IUnitOfWork unitOfWork,
            IBusHandler busHandler,
            INotificationHandler notificationHandler)
        {
            this.unitOfWork = unitOfWork;
            this.busHandler = busHandler;
            this.notificationHandler = notificationHandler;
        }

        protected async Task NotifyValidationErrors(
            ValidationResult validationResult,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Notification notification;
            foreach (ValidationFailure error in validationResult.Errors)
            {
                notification = new ValidationNotification(error.PropertyName, error.ErrorMessage);

                await notificationHandler.Handle(notification, cancellationToken);
                await busHandler.RaiseEvent(notification);
            }
        }

        public async Task<bool> Commit()
        {
            if (notificationHandler.HasNotifications())
            {
                return false;
            }

            if (unitOfWork.Commit())
            {
                return true;
            }

            await busHandler.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }

        protected async Task<ValidationResult> ValidateCommandAsync<TValidator, TCommand>(
            TCommand command,
            CancellationToken cancellationToken = new CancellationToken())
            where TValidator : AbstractValidator<TCommand>, new()
            where TCommand : ICommand
            => await new TValidator().ValidateAsync(command, cancellationToken);

        protected async Task<ValidationResult> ValidateCommandAsync<TValidator, TCommand>(
            TValidator validator,
            TCommand command,
            CancellationToken cancellationToken = new CancellationToken())
            where TValidator : AbstractValidator<TCommand>, new()
            where TCommand : ICommand
            => await validator.ValidateAsync(command, cancellationToken);
    }
}
