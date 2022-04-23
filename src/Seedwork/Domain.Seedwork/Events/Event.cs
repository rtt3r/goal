using System;

namespace Goal.Domain.Seedwork.Events
{
    public abstract class Event : IEvent
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public string AggregateId { get; protected set; }
        public string EventType { get; protected set; }

        protected Event()
        {
            EventType = GetType().Name;
        }
    }
}
