namespace Goal.Application.Commands;

public interface ICommandSender
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default);

    Task Send<TCommand>(TCommand request, CancellationToken cancellationToken = default)
        where TCommand : ICommand;

    Task<object?> Send(object request, CancellationToken cancellationToken = default);
}
