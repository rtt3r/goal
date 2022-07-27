using System.Threading;
using System.Threading.Tasks;

namespace Goal.Seedwork.Domain.Events
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}
