namespace Goal.Seedwork.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResultStatus Status { get; private set; }
        public bool IsSuccess() => Status == CommandResultStatus.Success;
        public bool IsValidationError() => Status == CommandResultStatus.ValidationError;
        public bool IsDomainError() => Status == CommandResultStatus.DomainError;
        public bool IsInternalError() => Status == CommandResultStatus.InternalError;
        public bool IsExternalError() => Status == CommandResultStatus.ExternalError;

        public static ICommandResult Success()
            => new CommandResult { Status = CommandResultStatus.Success };

        public static ICommandResult<T> Success<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.Success, Data = result };

        public static ICommandResult ValidationError()
            => new CommandResult { Status = CommandResultStatus.ValidationError };

        public static ICommandResult<T> ValidationError<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.ValidationError, Data = result };

        public static ICommandResult DomainError()
            => new CommandResult { Status = CommandResultStatus.DomainError };

        public static ICommandResult<T> DomainError<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.DomainError, Data = result };

        public static ICommandResult InternalError()
            => new CommandResult { Status = CommandResultStatus.InternalError };

        public static ICommandResult<T> InternalError<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.InternalError, Data = result };

        public static ICommandResult ExternalError()
            => new CommandResult { Status = CommandResultStatus.ExternalError };

        public static ICommandResult<T> ExternalError<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.ExternalError, Data = result };
    }
}
