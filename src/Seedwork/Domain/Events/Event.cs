using System;

namespace Goal.Seedwork.Domain.Events;

public abstract record Event(string AggregateId, string EventType) : IEvent
{
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
}
