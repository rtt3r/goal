using System.Threading.Tasks;
using Goal.Domain.Commands;
using Goal.Domain.Events;

namespace Goal.Domain.Bus
{
    public interface IBusHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
