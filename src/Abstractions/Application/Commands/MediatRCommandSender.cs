using MediatR;

namespace Goal.Application.Commands;

internal sealed class MediatRCommandSender(IMediator mediator) : ICommandSender
{
    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        => mediator.Send<TResponse>(command, cancellationToken);

    public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
        => mediator.Send(command, cancellationToken);

    public Task<object?> Send(object command, CancellationToken cancellationToken = default)
        => mediator.Send(command, cancellationToken);
}