using MediatR;

namespace Goal.Application.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent
{
}
