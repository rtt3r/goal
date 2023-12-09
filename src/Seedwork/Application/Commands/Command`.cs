namespace Goal.Seedwork.Application.Commands;

public abstract record Command<T> : ICommand<T>
    where T : ICommandResult
{
}
