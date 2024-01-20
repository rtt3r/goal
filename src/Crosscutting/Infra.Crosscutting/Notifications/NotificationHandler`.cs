using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Infra.Crosscutting.Notifications;

public abstract class NotificationHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : class, INotification
{
    private readonly ICollection<TNotification> notifications;

    protected NotificationHandler()
    {
        notifications = new List<TNotification>();
    }

    public virtual async Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        notifications.Add(notification);

        await Task.CompletedTask;
    }

    public virtual IEnumerable<TNotification> GetNotifications() => notifications;

    public async Task<IEnumerable<TNotification>> GetNotificationsAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Task.FromResult(GetNotifications());
    }
}
