namespace Goal.Domain.Seedwork.Messages
{
    public abstract class Message<T> : Message, IMessage<T>
    {
        protected Message()
            : base()
        {
        }
    }
}
