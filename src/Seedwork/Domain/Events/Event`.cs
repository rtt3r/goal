namespace Goal.Seedwork.Domain.Events
{
    public abstract class Event<T> : Event, IEvent<T>
    {
        public T Data { get; protected set; }

        protected Event(T data)
            : base()
        {
            Data = data;
        }

        protected Event()
            : base()
        {
        }
    }
}
