using MediatR;

namespace Goal.Seedwork.Application.Commands;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : ICommandResult
{
}
