using Goal.Domain.Seedwork.Commands;
using MediatR;

namespace Goal.Application.Seedwork.Handlers
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : ICommandResult
    {

    }
}
