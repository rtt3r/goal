using System;

namespace Goal.Domain.Seedwork.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event @event, string data, string user)
        {
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Data = data;
            User = user;
        }

        // EF Constructor
        protected StoredEvent() { }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}
