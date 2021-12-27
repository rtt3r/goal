using System;
using FluentValidation.Results;
using Goal.Domain.Events;

namespace Goal.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
