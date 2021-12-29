using System;
using FluentValidation.Results;
using Goal.Domain.Seedwork.Messages;

namespace Goal.Domain.Seedwork.Commands
{
    public abstract class Command<T> : Message<T>, ICommand
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();
    }
}
