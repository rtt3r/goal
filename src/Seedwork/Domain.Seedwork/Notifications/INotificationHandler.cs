using System.Collections.Generic;
using MediatR;

namespace Goal.Domain.Notifications
{
    public interface INotificationHandler : INotificationHandler<Notification>
    {
        List<Notification> GetNotifications();
        bool HasNotifications();
    }
}
