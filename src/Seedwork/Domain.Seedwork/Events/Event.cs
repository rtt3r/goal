using System;
using Goal.Domain.Seedwork.Messages;
using MediatR;

namespace Goal.Domain.Seedwork.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}
