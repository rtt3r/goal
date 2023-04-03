namespace Goal.Seedwork.Infra.Crosscutting.Notifications;

public interface INotification
{
    NotificationType Type { get; }
    string Code { get; }
    string Message { get; }
    string ParamName { get; }
}
