using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Infra.Crosscutting.Notifications;

public interface INotificationHandler<TNotification>
    where TNotification : class, INotification
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default);
    Task<IEnumerable<TNotification>> GetNotificationsAsync(CancellationToken cancellationToken = default);
    IEnumerable<TNotification> GetNotifications();
}
