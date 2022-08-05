using MediatR;

namespace Goal.Seedwork.Application.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : class, IEvent
    {
    }
}
