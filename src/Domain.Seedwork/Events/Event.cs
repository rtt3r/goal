using System;
using MediatR;

namespace Goal.Domain.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}
