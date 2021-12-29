using System;

namespace Goal.Domain.Seedwork.Messages
{
    public interface IMessage
    {
        Guid AggregateId { get; }
        string MessageType { get; }
    }


}
