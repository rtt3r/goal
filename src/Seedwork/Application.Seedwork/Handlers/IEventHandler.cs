using Goal.Domain.Seedwork.Events;
using MediatR;

namespace Goal.Application.Seedwork.Handlers
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}
