namespace Goal.Seedwork.Application.Commands
{
    public class CommandResult<T> : CommandResult, ICommandResult<T>
    {
        internal CommandResult()
            : base()
        {
        }

        public T Data { get; set; }
    }
}
