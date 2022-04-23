namespace Goal.Domain.Seedwork.Events
{
    public class StoredEvent<T> : Event<T>
    {
        public string User { get; private set; }

        public StoredEvent(T data, string user)
            : base(data)
        {
            User = user;
        }

        // EF Constructor
        protected StoredEvent() : base() { }
    }
}
