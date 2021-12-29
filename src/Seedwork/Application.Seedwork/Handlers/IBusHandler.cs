using System.Threading.Tasks;
using Goal.Domain.Seedwork.Commands;
using Goal.Domain.Seedwork.Events;

namespace Goal.Application.Seedwork.Handlers
{
    public interface IBusHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
