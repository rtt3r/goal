using System;

namespace Goal.Seedwork.Domain.Messages
{
    public abstract class Message : IMessage
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
