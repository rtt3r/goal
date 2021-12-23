using System;
using MediatR;

namespace Vantage.Domain.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}
