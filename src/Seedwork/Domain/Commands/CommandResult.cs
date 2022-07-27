using System.Collections.Generic;
using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool IsSucceeded { get; private set; }
        public bool HasInformation => HasNotification(NotificationType.Information);
        public bool HasInputValidation => HasNotification(NotificationType.InputValidation);
        public bool HasDomainViolation => HasNotification(NotificationType.DomainViolation);
        public bool HasInternalError => HasNotification(NotificationType.InternalError);
        public bool HasExternalError => HasNotification(NotificationType.ExternalError);
        public ICollection<INotification> Notifications { get; } = new List<INotification>();

        private bool HasNotification(NotificationType type)
            => Notifications.Any(n => n.Type == type);

        public static ICommandResult Success()
            => new CommandResult { IsSucceeded = true };

        public static ICommandResult<T> Success<T>(T result)
            => new CommandResult<T> { IsSucceeded = true, Data = result };

        public static ICommandResult Fail()
            => new CommandResult { IsSucceeded = false };

        public static ICommandResult<T> Fail<T>(T result)
            => new CommandResult<T> { IsSucceeded = false, Data = result };
    }
}
