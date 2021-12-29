using Goal.Domain.Messages;

namespace Goal.Application.Handlers
{
    public interface IMessageHandler<in TMessage>
        where TMessage : Message
    {
        void Handle(TMessage message);
    }
}
