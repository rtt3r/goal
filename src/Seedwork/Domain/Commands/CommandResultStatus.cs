namespace Goal.Seedwork.Domain.Commands
{
    public enum CommandResultStatus
    {
        Success,
        ContractViolation,
        DomainViolation,
        InternalError,
        ExternalError,
    }
}
