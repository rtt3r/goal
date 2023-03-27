using System;
using System.Collections.Generic;
using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Application.Commands
{
    public class CommandResult : ICommandResult
    {
        protected CommandResult()
        {
        }

        public bool IsSucceeded { get; private set; }
        public IEnumerable<Notification> Notifications { get; private set; } = new List<Notification>();

        public static ICommandResult Success(params Notification[] notifications)
        {
            if (notifications.Any(p => p.Type != NotificationType.Information))
            {
                throw new InvalidOperationException("For 'Success' result only notifications of type 'Information' are accepted.");
            }

            return new CommandResult
            {
                IsSucceeded = true,
                Notifications = notifications
            };
        }

        public static ICommandResult<TData> Success<TData>(TData data, params Notification[] notifications)
        {
            if (notifications.Any(p => p.Type != NotificationType.Information))
            {
                throw new InvalidOperationException("For 'Success' result only notifications of type 'Information' are accepted.");
            }

            return new CommandResult<TData>
            {
                IsSucceeded = true,
                Data = data,
                Notifications = notifications
            };
        }

        public static ICommandResult Failure(params Notification[] notifications)
        {
            if (!notifications.Any(n => n.Type != NotificationType.Information))
            {
                throw new InvalidOperationException("For 'Failure' result it's necessary to report failure notifications.");
            }

            return new CommandResult
            {
                IsSucceeded = false,
                Notifications = notifications
            };
        }

        public static ICommandResult Failure(IEnumerable<Notification> notifications)
            => Failure(notifications.ToArray());

        public static ICommandResult<TData> Failure<TData>(TData data, IEnumerable<Notification> notifications)
        {
            if (!notifications.Any(n => n.Type != NotificationType.Information))
            {
                throw new InvalidOperationException("For 'Failure' result it's necessary to report failure notifications.");
            }

            return new CommandResult<TData>
            {
                IsSucceeded = false,
                Data = data,
                Notifications = notifications
            };
        }

        public bool HasDomainViolation()
            => HasNotificationsOf(NotificationType.DomainViolation);

        public bool HasExternalError()
            => HasNotificationsOf(NotificationType.ExternalError);

        public bool HasInternalError()
            => HasNotificationsOf(NotificationType.InternalError);

        public bool HasInformation()
            => HasNotificationsOf(NotificationType.Information);

        public bool HasInputValidation()
            => HasNotificationsOf(NotificationType.InputValidation);

        public bool HasResourceNotFound()
            => HasNotificationsOf(NotificationType.ResourceNotFound);

        private bool HasNotificationsOf(NotificationType type)
            => Notifications.Any(p => p.Type == type);
    }
}
