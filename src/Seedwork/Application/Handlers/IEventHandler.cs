using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Domain.Events;

namespace Goal.Seedwork.Application.Handlers
{
    public interface IEventHandler
    {
        Task RaiseEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent;
    }
}
