using System;
using Goal.Domain.Messages;
using MediatR;

namespace Goal.Domain.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}
