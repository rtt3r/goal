using Vantage.Domain.Bus;
using Vantage.Domain.Commands;
using Vantage.Domain.Notifications;

namespace Vantage.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly IDomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, IDomainNotificationHandler notifications)
        {
            _uow = uow;
            _notifications = notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (FluentValidation.Results.ValidationFailure error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications())
            {
                return false;
            }

            if (_uow.Commit())
            {
                return true;
            }

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
