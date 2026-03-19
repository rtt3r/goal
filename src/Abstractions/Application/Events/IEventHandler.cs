namespace Goal.Application.Events;

public interface IEventHandler<in TEvent>
    where TEvent : class, IEvent
{
}