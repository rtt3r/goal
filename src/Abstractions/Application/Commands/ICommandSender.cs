using ConduitR.Abstractions;

namespace Goal.Application.Commands;

public interface ICommandSender
{
    ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
}

public sealed class CommandSender(IMediator mediator) : ICommandSender
{
    public ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        => mediator.Send(command, cancellationToken);
}