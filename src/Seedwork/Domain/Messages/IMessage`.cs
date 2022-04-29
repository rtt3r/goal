using MediatR;

namespace Goal.Seedwork.Domain.Messages
{
    public interface IMessage<out T> : IMessage, IRequest<T>
    {
    }
}
