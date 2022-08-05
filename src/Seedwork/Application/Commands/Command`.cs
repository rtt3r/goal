namespace Goal.Seedwork.Application.Commands
{
    public abstract class Command<T> : Command, ICommand<T>
        where T : ICommandResult
    {
    }
}
