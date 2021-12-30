namespace Goal.Domain.Seedwork.Commands
{
    public interface ICommandResult<T> : ICommandResult
    {
        T Data { get; set; }
    }
}
