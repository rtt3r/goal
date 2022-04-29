using System;
using MediatR;

namespace Goal.Seedwork.Domain.Messages
{
    public interface IMessage : IBaseRequest
    {
        Guid AggregateId { get; }
        string MessageType { get; }
    }
}
