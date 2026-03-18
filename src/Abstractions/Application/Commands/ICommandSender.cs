namespace Goal.Application.Commands;

public interface ICommandSender
{
    ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
}
