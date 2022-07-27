using System;

namespace Goal.Seedwork.Domain.Events
{
    public interface IEvent
    {
        string AggregateId { get; }
        string EventType { get; }
        DateTimeOffset Timestamp { get; }
    }
}
