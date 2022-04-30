namespace Goal.Seedwork.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResultStatus Status { get; private set; }
        public bool IsSuccess() => Status == CommandResultStatus.Success;
        public bool IsContractViolation() => Status == CommandResultStatus.ContractViolation;
        public bool IsDomainViolation() => Status == CommandResultStatus.DomainViolation;
        public bool IsInternalError() => Status == CommandResultStatus.InternalError;
        public bool IsExternalError() => Status == CommandResultStatus.ExternalError;

        public static ICommandResult Success()
            => new CommandResult { Status = CommandResultStatus.Success };

        public static ICommandResult<T> Success<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.Success, Data = result };

        public static ICommandResult ContractViolation()
            => new CommandResult { Status = CommandResultStatus.ContractViolation };

        public static ICommandResult<T> ContractViolation<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.ContractViolation, Data = result };

        public static ICommandResult DomainViolation()
            => new CommandResult { Status = CommandResultStatus.DomainViolation };

        public static ICommandResult<T> DomainViolation<T>(T result)
            => new CommandResult<T> { Status = CommandResultStatus.DomainViolation, Data = result };

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
