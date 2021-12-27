namespace Goal.Domain.Messages
{
    public interface IMessageHandler<in TMessage>
        where TMessage : Message
    {
        void Handle(TMessage message);
    }
}
