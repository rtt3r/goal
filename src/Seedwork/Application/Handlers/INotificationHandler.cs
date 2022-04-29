using System.Collections.Generic;
using Goal.Seedwork.Domain.Notifications;
using MediatR;

namespace Goal.Seedwork.Application.Handlers
{
    public interface INotificationHandler : INotificationHandler<Notification>
    {
        IEnumerable<Notification> GetNotifications();
        bool HasNotifications();
    }
}
