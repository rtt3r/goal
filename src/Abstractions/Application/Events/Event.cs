using System;

namespace Goal.Application.Events;

public abstract class Event(string aggregateId, string eventType)
    : IEvent
{
    public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.UtcNow;
    public string AggregateId { get; protected set; } = aggregateId;
    public string EventType { get; protected set; } = eventType;
}
