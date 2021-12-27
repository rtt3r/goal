using System;
using Goal.Domain.Events;

namespace Goal.Domain.Notifications
{
    public class Notification : Event
    {
        public Guid NotificationId { get; } = Guid.NewGuid();
        public int Version { get; private set; } = 1;
        public string Key { get; private set; }
        public string Value { get; private set; }

        public Notification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
