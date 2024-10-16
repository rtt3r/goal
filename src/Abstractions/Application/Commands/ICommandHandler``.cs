using MediatR;

namespace Goal.Application.Commands;

public interface ICommandHandler<in TCommand, TResult> : ICommandHandler<TCommand>, IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}
