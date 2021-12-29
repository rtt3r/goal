using Goal.Domain.Seedwork.Messages;

namespace Goal.Application.Seedwork.Handlers
{
    public interface IMessageHandler<in TMessage>
        where TMessage : Message
    {
        void Handle(TMessage message);
    }
}
