using System;
using MediatR;

namespace Goal.Domain.Events;

public interface IEvent : INotification
{
    string AggregateId { get; }
    string EventType { get; }
    DateTimeOffset Timestamp { get; }
}
