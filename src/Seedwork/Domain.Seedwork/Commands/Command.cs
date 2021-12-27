using System;
using FluentValidation;
using FluentValidation.Results;
using Goal.Domain.Messages;

namespace Goal.Domain.Commands
{
    public abstract class Command : Message
    {
        protected abstract AbstractValidator<Command> Validator { get; }

        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
        public ValidationResult ValidationResult => Validator.Validate(this);
        public virtual bool IsValid => ValidationResult.IsValid;
    }
}
