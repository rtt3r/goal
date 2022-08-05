namespace Goal.Seedwork.Application.Events
{
    public interface IEventStore
    {
        void Save<T>(T @event) where T : IEvent;
    }
}
