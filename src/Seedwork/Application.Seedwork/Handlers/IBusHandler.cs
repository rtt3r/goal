using System.Threading.Tasks;
using Goal.Domain.Seedwork.Commands;
using Goal.Domain.Seedwork.Events;

namespace Goal.Application.Seedwork.Handlers
{
    public interface IBusHandler
    {
        Task<bool> SendCommand<TCommand>(TCommand command) where TCommand : Command<bool>;
        Task<TResult> SendCommand<TCommand, TResult>(TCommand command) where TCommand : Command<TResult>;
        Task RaiseEvent<TCommand>(TCommand @event) where TCommand : Event;
    }
}
