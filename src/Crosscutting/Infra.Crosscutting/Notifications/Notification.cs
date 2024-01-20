using System;

namespace Goal.Infra.Crosscutting.Notifications;

public record Notification(NotificationType Type, string Code, string Message, string? ParamName) : INotification
{
    public static Notification Information(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        return new(NotificationType.Information, code, message, null);
    }

    public static Notification InternalError(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        return new(NotificationType.InternalError, code, message, null);
    }

    public static Notification ExternalError(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        return new(NotificationType.ExternalError, code, message, null);
    }

    public static Notification InputValidation(string code, string message, string paramName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        ArgumentException.ThrowIfNullOrWhiteSpace(paramName);

        return new(NotificationType.InputValidation, code, message, paramName);
    }

    public static Notification DomainViolation(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        return new(NotificationType.DomainViolation, code, message, null);
    }

    public static Notification ResourceNotFound(string code, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        return new(NotificationType.ResourceNotFound, code, message, null);
    }
}
