using System;
using MediatR;

namespace Goal.Seedwork.Application.Events
{
    public interface IEvent : INotification
    {
        string AggregateId { get; }
        string EventType { get; }
        DateTimeOffset Timestamp { get; }
    }
}
