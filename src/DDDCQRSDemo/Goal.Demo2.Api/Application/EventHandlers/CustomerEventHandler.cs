using System.Threading;
using System.Threading.Tasks;
using Goal.Demo2.Api.Application.Events;
using MediatR;

namespace Goal.Demo2.Api.Application.EventHandlers
{
    public class CustomerEventHandler :
        INotificationHandler<CustomerRegisteredEvent>,
        INotificationHandler<CustomerUpdatedEvent>,
        INotificationHandler<CustomerRemovedEvent>
    {
        public Task Handle(CustomerUpdatedEvent message, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(CustomerRegisteredEvent message, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(CustomerRemovedEvent message, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
