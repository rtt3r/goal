namespace Goal.Domain.Events;

public interface IEventStore
{
    void Save<T>(T @event) where T : IEvent;
}
