using System;
using Goal.Domain.Seedwork.Messages;

namespace Goal.Domain.Seedwork.Commands
{
    public abstract class Command : Message, ICommand
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}
