namespace Goal.Application.Events;

public interface IEvent
{
    string AggregateId { get; }
    string EventType { get; }
    DateTimeOffset Timestamp { get; }
}
