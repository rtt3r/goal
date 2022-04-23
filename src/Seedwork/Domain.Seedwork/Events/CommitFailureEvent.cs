namespace Goal.Domain.Seedwork.Events
{
    public class CommitFailureEvent : Event
    {
        public CommitFailureEvent(string message)
            : base()
        {
            Message = message;
        }

        public string Message { get; }
    }
}
