using System;
using Goal.DemoCqrs.Api.Commands.Validators;

namespace Goal.DemoCqrs.Api.Commands
{
    public class UpdatePersonCommand : PersonCommand
    {
        public Guid PersonId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePersonCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
