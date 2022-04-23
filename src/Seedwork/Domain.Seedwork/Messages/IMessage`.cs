using MediatR;

namespace Goal.Domain.Seedwork.Messages
{
    public interface IMessage<out T> : IMessage, IRequest<T>
    {
    }
}
