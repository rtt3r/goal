namespace Goal.Seedwork.Domain.Commands
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
