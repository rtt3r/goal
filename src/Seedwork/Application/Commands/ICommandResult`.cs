namespace Goal.Application.Abstractions.Commands;

public interface ICommandResult<T> : ICommandResult
{
    T? Data { get; }
}
