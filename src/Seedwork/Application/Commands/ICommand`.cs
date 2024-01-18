using MediatR;

namespace Goal.Application.Abstractions.Commands;

public interface ICommand<out TResult> : ICommand, IRequest<TResult>
    where TResult : ICommandResult
{
}
