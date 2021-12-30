namespace Goal.Domain.Seedwork.Commands
{
    public abstract class Command<T> : Command, ICommand<T>
        where T : ICommandResult
    {
    }
}
