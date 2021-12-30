using System;
using Goal.Domain.Seedwork.Messages;

namespace Goal.Domain.Seedwork.Commands
{
    public interface ICommand : IMessage
    {
        public DateTimeOffset Timestamp { get; }
    }
}
