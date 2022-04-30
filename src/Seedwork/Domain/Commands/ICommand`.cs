using MediatR;

namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommand<out TResult> : ICommand, IRequest<TResult>
        where TResult : ICommandResult
    {
    }
}
