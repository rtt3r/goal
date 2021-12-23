using System;
using FluentValidation.Results;
using Vantage.Domain.Events;

namespace Vantage.Domain.Commands
{
    public abstract class Command : Message
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
