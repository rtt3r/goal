namespace Goal.Application.Commands;

public interface ICommandResult<T> : ICommandResult
{
    T? Data { get; }
}
