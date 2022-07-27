using System.Threading.Tasks;
using Goal.Seedwork.Domain.Events;

namespace Goal.Seedwork.Application.Handlers
{
    public interface IBusHandler
    {
        Task SendAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
