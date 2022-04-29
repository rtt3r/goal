namespace Goal.Seedwork.Domain.Commands
{
    public abstract class Command<T> : Command, ICommand<T>
        where T : ICommandResult
    {
    }
}
