namespace Goal.Seedwork.Domain.Messages
{
    public abstract class Message<T> : Message, IMessage<T>
    {
        protected Message()
            : base()
        {
        }
    }
}
