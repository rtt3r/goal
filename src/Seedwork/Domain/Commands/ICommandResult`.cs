namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommandResult<T> : ICommandResult
    {
        T Data { get; set; }
    }
}
