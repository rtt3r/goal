using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Domain.Seedwork.Notifications;

namespace Goal.Application.Seedwork.Handlers
{
    public sealed class NotificationHandler : INotificationHandler
    {
        private readonly List<Notification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public Task Handle(Notification message, CancellationToken cancellationToken)
        {
            Handle(message);
            return Task.CompletedTask;
        }

        public void Handle(Notification message) => _notifications.Add(message);

        public IEnumerable<Notification> GetNotifications() => _notifications;

        public bool HasNotifications() => GetNotifications().Any();
    }
}
