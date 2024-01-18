using MediatR;

namespace Goal.Application.Commands;

public interface ICommand<out TResult> : ICommand, IRequest<TResult>
    where TResult : ICommandResult
{
}
