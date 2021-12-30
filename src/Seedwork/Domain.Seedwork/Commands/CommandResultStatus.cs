namespace Goal.Domain.Seedwork.Commands
{
    public enum CommandResultStatus
    {
        Success,
        ValidationError,
        DomainError,
        InternalError,
        ExternalError,
    }
}
