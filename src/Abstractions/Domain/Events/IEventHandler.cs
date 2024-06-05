using MediatR;

namespace Goal.Domain.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent
{
}
