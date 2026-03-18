using ConduitR.Abstractions;

namespace Goal.Application.Commands;

public sealed class ConduitRCommandSender(IMediator mediator) : ICommandSender
{
    public ValueTask<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        => mediator.Send(command, cancellationToken);
}