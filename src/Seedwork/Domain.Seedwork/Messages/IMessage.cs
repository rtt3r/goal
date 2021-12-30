using System;
using MediatR;

namespace Goal.Domain.Seedwork.Messages
{
    public interface IMessage : IBaseRequest
    {
        Guid AggregateId { get; }
        string MessageType { get; }
    }
}
