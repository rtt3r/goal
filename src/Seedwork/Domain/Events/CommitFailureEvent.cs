namespace Goal.Seedwork.Domain.Events
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
