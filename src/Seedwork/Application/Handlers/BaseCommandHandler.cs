using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Goal.Seedwork.Domain;
using Goal.Seedwork.Domain.Commands;
using Goal.Seedwork.Domain.Events;
using Goal.Seedwork.Domain.Notifications;

namespace Goal.Seedwork.Application.Handlers
{
    public abstract class BaseCommandHandler
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IBusHandler busHandler;
        protected readonly INotificationHandler notificationHandler;

        protected BaseCommandHandler(
            IUnitOfWork unitOfWork,
            IBusHandler busHandler,
            INotificationHandler notificationHandler)
        {
            this.unitOfWork = unitOfWork;
            this.busHandler = busHandler;
            this.notificationHandler = notificationHandler;
        }

        protected async Task NotifyViolations(
            ValidationResult validationResult,
            CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (ValidationFailure error in validationResult.Errors)
            {
                await notificationHandler.Handle(
                    new ViolationNotification(error.PropertyName, error.ErrorMessage),
                    cancellationToken);
            }
        }

        public async Task<bool> Commit(CancellationToken cancellationToken = new CancellationToken())
        {
            if (notificationHandler.HasNotifications())
            {
                return false;
            }

            if (unitOfWork.Commit())
            {
                return true;
            }

            await notificationHandler.Handle(
                new Notification("COMMIT", "We had a problem during saving your data."),
                cancellationToken);

            await busHandler.RaiseEvent(new CommitFailureEvent("We had a problem during saving your data."));

            return false;
        }

        protected static async Task<ValidationResult> ValidateCommandAsync<TValidator, TCommand>(
            TCommand command,
            CancellationToken cancellationToken = new CancellationToken())
            where TValidator : AbstractValidator<TCommand>, new()
            where TCommand : ICommand
            => await new TValidator().ValidateAsync(command, cancellationToken);

        protected static async Task<ValidationResult> ValidateCommandAsync<TValidator, TCommand>(
            TValidator validator,
            TCommand command,
            CancellationToken cancellationToken = new CancellationToken())
            where TValidator : AbstractValidator<TCommand>, new()
            where TCommand : ICommand
            => await validator.ValidateAsync(command, cancellationToken);
    }
}
