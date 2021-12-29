using System.Threading.Tasks;
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

        protected async Task NotifyValidationErrors(ICommand message)
        {
            foreach (ValidationFailure error in message.ValidationResult.Errors)
            {
                await busHandler.RaiseEvent(new Notification(message.MessageType, error.ErrorMessage));
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

            await busHandler.RaiseEvent(new Notification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
