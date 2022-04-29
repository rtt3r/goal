using System;
using Goal.Seedwork.Domain.Messages;

namespace Goal.Seedwork.Domain.Commands
{
    public interface ICommand : IMessage
    {
        public DateTimeOffset Timestamp { get; }
    }
}
