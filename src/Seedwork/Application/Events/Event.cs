using System;

namespace Goal.Seedwork.Application.Events
{
    public abstract class Event : IEvent
    {
        public DateTimeOffset Timestamp { get; protected set; } = DateTimeOffset.UtcNow;
        public string AggregateId { get; protected set; }
        public string EventType { get; protected set; }

        protected Event()
        {
            EventType = GetType().Name;
        }
    }
}
