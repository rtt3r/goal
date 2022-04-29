using Goal.Seedwork.Domain.Commands;
using MediatR;

namespace Goal.Seedwork.Application.Handlers
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : ICommandResult
    {

    }
}
