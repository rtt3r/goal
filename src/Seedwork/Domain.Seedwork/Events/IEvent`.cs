namespace Goal.Domain.Seedwork.Events
{
    public interface IEvent<out T> : IEvent
    {
        T Data { get; }
    }
}
