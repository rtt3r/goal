using System.Threading.Tasks;
using Goal.Seedwork.Domain.Commands;
using Goal.Seedwork.Domain.Events;

namespace Goal.Seedwork.Application.Handlers
{
    public interface IBusHandler
    {
        Task RaiseEvent<TEvent>(TEvent @event) where TEvent : IEvent;
        Task<ICommandResult<TResult>> SendCommand<TCommand, TResult>(TCommand command) where TCommand : ICommand<ICommandResult<TResult>>;
        Task<ICommandResult> SendCommand<TCommand>(TCommand command) where TCommand : ICommand<ICommandResult>;
    }
}
