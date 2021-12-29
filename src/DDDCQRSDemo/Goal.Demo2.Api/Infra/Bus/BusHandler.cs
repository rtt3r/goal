using System.Threading.Tasks;
using Goal.Application.Seedwork.Handlers;
using Goal.Domain.Seedwork.Commands;
using Goal.Domain.Seedwork.Events;
using MediatR;

namespace Goal.Demo2.Api.Infra.Bus
{
    public sealed class InMemoryBusHandler : IBusHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBusHandler(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task<bool> SendCommand<TCommand>(TCommand command)
            where TCommand : Command<bool>
            => SendCommand<TCommand, bool>(command);

        public Task<TResult> SendCommand<TCommand, TResult>(TCommand command)
            where TCommand : Command<TResult>
            => _mediator.Send(command);

        public Task RaiseEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
            {
                _eventStore?.Save(@event);
            }

            return _mediator.Publish(@event);
        }
    }
}
