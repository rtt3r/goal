using System;
using Vantage.Domain.Events;

namespace Vantage.Domain.Notifications
{
    public class DomainNotification : Event
    {
        public Guid NotificationId { get; } = Guid.NewGuid();
        public int Version { get; private set; } = 1;
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
