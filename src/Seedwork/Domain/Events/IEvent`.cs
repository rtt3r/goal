namespace Goal.Seedwork.Domain.Events
{
    public interface IEvent<out T> : IEvent
    {
        T Data { get; }
    }
}
