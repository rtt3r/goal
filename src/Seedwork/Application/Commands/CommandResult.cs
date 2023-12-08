using System;
using System.Collections.Generic;
using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Goal.Seedwork.Application.Commands;

public record CommandResult(bool IsSucceeded, IEnumerable<Notification> Notifications) : ICommandResult
{
    public static ICommandResult Success(params Notification[] notifications)
    {
        return notifications.All(p => p.Type != NotificationType.Information)
            ? new CommandResult(true, notifications)
            : throw new InvalidOperationException("For 'Success' result only notifications of type 'Information' are accepted.");
    }

    public static ICommandResult<TData> Success<TData>(TData data, params Notification[] notifications)
    {
        return notifications.All(p => p.Type != NotificationType.Information)
            ? new CommandResult<TData>(true, data, notifications)
            : throw new InvalidOperationException("For 'Success' result only notifications of type 'Information' are accepted.");
    }

    public static ICommandResult Failure(params Notification[] notifications)
    {
        return notifications.Any(n => n.Type != NotificationType.Information)
            ? new CommandResult(false, notifications)
            : throw new InvalidOperationException("For 'Failure' result it's necessary to report failure notifications.");
    }

    public static ICommandResult Failure(IEnumerable<Notification> notifications)
        => Failure(notifications.ToArray());

    public static ICommandResult<TData> Failure<TData>(TData data, IEnumerable<Notification> notifications)
    {
        return notifications.Any(n => n.Type != NotificationType.Information)
            ? new CommandResult<TData>(false, data, notifications)
            : throw new InvalidOperationException("For 'Failure' result it's necessary to report failure notifications.");
    }

    public bool HasDomainViolation
        => HasNotificationsOf(NotificationType.DomainViolation);

    public bool HasExternalError
        => HasNotificationsOf(NotificationType.ExternalError);

    public bool HasInternalError
        => HasNotificationsOf(NotificationType.InternalError);

    public bool HasInformation
        => HasNotificationsOf(NotificationType.Information);

    public bool HasInputValidation
        => HasNotificationsOf(NotificationType.InputValidation);

    public bool HasResourceNotFound
        => HasNotificationsOf(NotificationType.ResourceNotFound);

    private bool HasNotificationsOf(NotificationType type)
        => Notifications.Any(p => p.Type == type);
}
