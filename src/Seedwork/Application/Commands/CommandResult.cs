namespace Goal.Seedwork.Application.Commands
{
    public class CommandResult : ICommandResult
    {
        protected CommandResult()
        {
        }

        public bool IsSucceeded { get; private set; }

        public static ICommandResult Success()
            => new CommandResult { IsSucceeded = true };

        public static ICommandResult<TData> Success<TData>(TData data)
            => new CommandResult<TData> { IsSucceeded = true, Data = data };

        public static ICommandResult Fail()
            => new CommandResult { IsSucceeded = false };

        public static ICommandResult<TData> Fail<TData>(TData data)
            => new CommandResult<TData> { IsSucceeded = false, Data = data };
    }
}
