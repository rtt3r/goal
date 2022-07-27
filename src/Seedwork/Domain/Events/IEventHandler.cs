using MediatR;

namespace Goal.Seedwork.Domain.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    {
    }
}
