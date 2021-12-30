using Goal.Domain.Seedwork.Messages;

namespace Goal.Domain.Seedwork.Commands
{
    public interface ICommand<TResult> : ICommand, IMessage<TResult>
        where TResult : ICommandResult
    {
    }
}
