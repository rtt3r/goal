using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Domain.Commands;

namespace Goal.Seedwork.Application.Handlers
{
    public interface ICommandHandler
    {
        Task<ICommandResult<TResult>> HandleCommand<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand<ICommandResult<TResult>>;

        Task<ICommandResult> HandleCommand<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand<ICommandResult>;
    }
}
