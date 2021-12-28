namespace Goal.DemoCqrs.Api.Commands.Validators
{
    public sealed class AddPersonCommandValidator : PersonCommandValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            ValidateFistName();
            ValidateLastName();
            ValidateCpf();
        }
    }
}
