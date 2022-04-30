namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommandResult
    {
        CommandResultStatus Status { get; }

        bool IsSuccess();
        bool IsContractViolation();
        bool IsDomainViolation();
        bool IsInternalError();
        bool IsExternalError();
    }
}
