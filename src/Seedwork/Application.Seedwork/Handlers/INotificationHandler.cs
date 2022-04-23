using System.Collections.Generic;
using Goal.Domain.Seedwork.Notifications;
using MediatR;

namespace Goal.Application.Seedwork.Handlers
{
    public interface INotificationHandler : INotificationHandler<Notification>
    {
        IEnumerable<Notification> GetNotifications();
        bool HasNotifications();
    }
}
