using FluentValidation.Results;
using Goal.Domain.Bus;
using Goal.Domain.Notifications;

namespace Goal.Domain.Commands
{
    public class CommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBusHandler busHandler;
        private readonly INotificationHandler notificationHandler;

        public CommandHandler(
            IUnitOfWork unitOfWork,
            IBusHandler busHandler,
            INotificationHandler notificationHandler)
        {
            this.unitOfWork = unitOfWork;
            this.busHandler = busHandler;
            this.notificationHandler = notificationHandler;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (ValidationFailure error in message.ValidationResult.Errors)
            {
                busHandler.RaiseEvent(new Notification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (notificationHandler.HasNotifications())
            {
                return false;
            }

            if (unitOfWork.Commit())
            {
                return true;
            }

            busHandler.RaiseEvent(new Notification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
