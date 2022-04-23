namespace Goal.Domain.Seedwork.Events
{
    public interface IEventStore
    {
        void Save<T>(T @event) where T : IEvent;
    }
}
