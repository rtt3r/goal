using System.Threading.Tasks;
using Goal.Seedwork.Domain.Events;
using MediatR;

namespace Goal.Seedwork.Application.Handlers
{
    public sealed class DefaultEventHandler : IEventHandler
    {
        private readonly IEventStore eventStore;
        private readonly IMediator mediator;

        public DefaultEventHandler(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = eventStore;
            this.mediator = mediator;
        }

        public Task RaiseEventAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            eventStore?.Save(@event);
            return mediator.Publish(@event);
        }
    }
}
