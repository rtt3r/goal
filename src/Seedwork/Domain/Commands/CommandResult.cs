using System.Collections.Generic;
using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        protected CommandResult(bool isSucceeded, ICollection<INotification> notifications)
        {
            IsSucceeded = isSucceeded;
            Notifications = notifications ?? new List<INotification>();
        }

        public bool IsSucceeded { get; private set; }
        public bool HasInformation => HasNotification(NotificationType.Information);
        public bool HasInputValidation => HasNotification(NotificationType.InputValidation);
        public bool HasDomainViolation => HasNotification(NotificationType.DomainViolation);
        public bool HasInternalError => HasNotification(NotificationType.InternalError);
        public bool HasExternalError => HasNotification(NotificationType.ExternalError);
        public ICollection<INotification> Notifications { get; private set; } = new List<INotification>();

        private bool HasNotification(NotificationType type)
            => Notifications.Any(n => n.Type == type);

        public static ICommandResult Success(params INotification[] notifications)
            => new CommandResult(true, notifications);

        public static ICommandResult<T> Success<T>(T result, params INotification[] notifications)
            => new CommandResult<T>(true, result, notifications);

        public static ICommandResult Fail(params INotification[] notifications)
            => new CommandResult(false, notifications);

        public static ICommandResult<T> Fail<T>(T result, params INotification[] notifications)
            => new CommandResult<T>(false, result, notifications);
    }
}
