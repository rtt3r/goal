using System.Threading.Tasks;
using Goal.Seedwork.Domain.Events;

namespace Goal.Seedwork.Application.Handlers
{
    public interface IEventHandler
    {
        Task RaiseEvent<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
