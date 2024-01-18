using MediatR;

namespace Goal.Domain.Abstractions.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : class, IEvent
{
}
