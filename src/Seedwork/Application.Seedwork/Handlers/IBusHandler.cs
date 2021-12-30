using System.Threading.Tasks;
using Goal.Domain.Seedwork.Commands;
using Goal.Domain.Seedwork.Events;

namespace Goal.Application.Seedwork.Handlers
{
    public interface IBusHandler
    {
        Task RaiseEvent<TEvent>(TEvent @event) where TEvent : Event;
        Task<ICommandResult<TResult>> SendCommand<TCommand, TResult>(TCommand command) where TCommand : ICommand<ICommandResult<TResult>>;
        Task<ICommandResult> SendCommand<TCommand>(TCommand command) where TCommand : ICommand<ICommandResult>;
    }
}
