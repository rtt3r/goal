using System;

namespace Goal.Seedwork.Infra.Crosscutting.Notifications;

public sealed class Notification : INotification
{
    public NotificationType Type { get; }
    public string Code { get; }
    public string Message { get; }
    public string ParamName { get; }

    private Notification(NotificationType type, string code, string message)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException($"'{nameof(code)}' cannot be null or empty.", nameof(code));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));
        }

        Type = type;
        Code = code;
        Message = message;
    }

    private Notification(NotificationType type, string code, string message, string paramName)
        : this(type, code, message)
    {
        if (string.IsNullOrWhiteSpace(paramName))
        {
            throw new ArgumentException($"'{nameof(paramName)}' cannot be null or empty.", nameof(paramName));
        }

        ParamName = paramName;
    }

    public static Notification Information(string code, string message)
        => new(NotificationType.Information, code, message);

    public static Notification InternalError(string code, string message)
        => new(NotificationType.InternalError, code, message);

    public static Notification ExternalError(string code, string message)
        => new(NotificationType.ExternalError, code, message);

    public static Notification InputValidation(string code, string message, string paramName)
        => new(NotificationType.InputValidation, code, message, paramName);

    public static Notification DomainViolation(string code, string message)
        => new(NotificationType.DomainViolation, code, message);

    public static Notification ResourceNotFound(string code, string message)
        => new(NotificationType.ResourceNotFound, code, message);
}
