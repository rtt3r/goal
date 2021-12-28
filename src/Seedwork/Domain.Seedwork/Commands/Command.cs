using System;
using FluentValidation.Results;
using Goal.Domain.Messages;

namespace Goal.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
