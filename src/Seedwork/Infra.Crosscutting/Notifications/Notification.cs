namespace Goal.Seedwork.Infra.Crosscutting.Notifications;

public record Notification(NotificationType Type, string Code, string Message, string? ParamName) : INotification
{
    public static Notification Information(string code, string message)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(code, nameof(code));
        Ensure.Argument.IsNotNullOrWhiteSpace(message, nameof(message));

        return new(NotificationType.Information, code, message, null);
    }

    public static Notification InternalError(string code, string message)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(code, nameof(code));
        Ensure.Argument.IsNotNullOrWhiteSpace(message, nameof(message));

        return new(NotificationType.InternalError, code, message, null);
    }

    public static Notification ExternalError(string code, string message)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(code, nameof(code));
        Ensure.Argument.IsNotNullOrWhiteSpace(message, nameof(message));

        return new(NotificationType.ExternalError, code, message, null);
    }

    public static Notification InputValidation(string code, string message, string paramName)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(code, nameof(code));
        Ensure.Argument.IsNotNullOrWhiteSpace(message, nameof(message));
        Ensure.Argument.IsNotNullOrWhiteSpace(paramName, nameof(paramName));

        return new(NotificationType.InputValidation, code, message, paramName);
    }

    public static Notification DomainViolation(string code, string message)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(code, nameof(code));
        Ensure.Argument.IsNotNullOrWhiteSpace(message, nameof(message));

        return new(NotificationType.DomainViolation, code, message, null);
    }

    public static Notification ResourceNotFound(string code, string message)
    {
        Ensure.Argument.IsNotNullOrWhiteSpace(code, nameof(code));
        Ensure.Argument.IsNotNullOrWhiteSpace(message, nameof(message));

        return new(NotificationType.ResourceNotFound, code, message, null);
    }
}
