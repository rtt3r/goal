namespace Goal.Seedwork.Application.Commands;

public interface ICommandResult<out T> : ICommandResult
{
    T? Data { get; }
}
