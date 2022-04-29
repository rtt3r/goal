using System;
using Goal.Seedwork.Domain.Messages;

namespace Goal.Seedwork.Domain.Commands
{
    public abstract class Command : Message, ICommand
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}
