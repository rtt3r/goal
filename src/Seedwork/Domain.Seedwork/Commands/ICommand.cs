using System;
using FluentValidation.Results;
using Goal.Domain.Seedwork.Messages;

namespace Goal.Domain.Seedwork.Commands
{
    public interface ICommand : IMessage
    {
        DateTimeOffset Timestamp { get; }
        ValidationResult ValidationResult { get; set; }

        bool IsValid();
    }
}
