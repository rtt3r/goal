using MediatR;

namespace Goal.Seedwork.Application.Commands
{
    public interface ICommand<out TResult> : ICommand, IRequest<TResult>
        where TResult : ICommandResult
    {
    }
}
