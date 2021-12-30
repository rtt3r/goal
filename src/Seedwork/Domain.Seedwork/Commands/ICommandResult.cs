namespace Goal.Domain.Seedwork.Commands
{
    public interface ICommandResult
    {
        CommandResultStatus Status { get; }

        bool IsSuccess();
        bool IsValidationError();
        bool IsDomainError();
        bool IsInternalError();
        bool IsExternalError();
    }
}
