namespace Goal.Seedwork.Application.Commands
{
    public abstract class Command<T> : ICommand<T>
        where T : ICommandResult
    {
    }
}
