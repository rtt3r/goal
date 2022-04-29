using Goal.Seedwork.Domain.Events;
using MediatR;

namespace Goal.Seedwork.Application.Handlers
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
    }
}
