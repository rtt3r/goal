using MediatR;

namespace Goal.Domain.Seedwork.Messages
{
    public interface IMessage<T> : IMessage, IRequest<T>
    {
    }
}
