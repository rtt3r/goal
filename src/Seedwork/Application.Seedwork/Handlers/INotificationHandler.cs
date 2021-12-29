using System.Collections.Generic;
using Goal.Domain.Notifications;
using MediatR;

namespace Goal.Application.Handlers
{
    public interface INotificationHandler : INotificationHandler<Notification>
    {
        List<Notification> GetNotifications();
        bool HasNotifications();
    }
}
