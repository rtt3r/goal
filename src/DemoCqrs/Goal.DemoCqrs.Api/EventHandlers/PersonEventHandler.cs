using System.Threading;
using System.Threading.Tasks;
using Goal.DemoCqrs.Api.Events;
using MediatR;

namespace Goal.DemoCqrs.Api.EventHandlers
{
    public class PersonEventHandler :
        INotificationHandler<PersonAddedEvent>,
        INotificationHandler<PersonUpdatedEvent>
    {
        public Task Handle(PersonUpdatedEvent message, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(PersonAddedEvent message, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
