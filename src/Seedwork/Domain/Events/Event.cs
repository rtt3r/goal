using System;

namespace Goal.Domain.Abstractions.Events;

public abstract record Event(string AggregateId, string EventType) : IEvent
{
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
}
