using Goal.DemoCqrs.Api.Commands.Validators;

namespace Goal.DemoCqrs.Api.Commands
{
    public class AddPersonCommand : PersonCommand<>
    {
        public override bool IsValid()
        {
            ValidationResult = new AddPersonCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
