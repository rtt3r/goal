using Goal.Seedwork.Domain.Messages;

namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommand<out TResult> : ICommand, IMessage<TResult>
        where TResult : ICommandResult
    {
    }
}
