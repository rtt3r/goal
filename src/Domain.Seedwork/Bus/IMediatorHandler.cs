using System.Threading.Tasks;
using Vantage.Domain.Commands;
using Vantage.Domain.Events;

namespace Vantage.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
