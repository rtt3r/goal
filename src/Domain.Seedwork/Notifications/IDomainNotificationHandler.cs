using System.Collections.Generic;
using MediatR;

namespace Goal.Domain.Notifications
{
    public interface IDomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        List<DomainNotification> GetNotifications();
        bool HasNotifications();
    }
}
