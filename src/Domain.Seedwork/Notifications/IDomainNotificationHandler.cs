using System.Collections.Generic;
using MediatR;

namespace Vantage.Domain.Notifications
{
    public interface IDomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        List<DomainNotification> GetNotifications();
        bool HasNotifications();
    }
}
