namespace Goal.DemoCqrs.Api.Commands.Validators
{
    public sealed class UpdatePersonCommandValidator : PersonCommandValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            ValidateFistName();
            ValidateLastName();
            ValidateCpf();
        }
    }
}
