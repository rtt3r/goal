namespace Goal.Infra.Crosscutting.Notifications;

public enum NotificationType
{
    Information,
    InputValidation,
    DomainViolation,
    ResourceNotFound,
    InternalError,
    ExternalError
}
