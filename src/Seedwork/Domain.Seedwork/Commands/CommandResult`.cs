namespace Goal.Domain.Seedwork.Commands
{
    public class CommandResult<T> : CommandResult, ICommandResult<T>
    {
        public T Data { get; set; }
    }
}
