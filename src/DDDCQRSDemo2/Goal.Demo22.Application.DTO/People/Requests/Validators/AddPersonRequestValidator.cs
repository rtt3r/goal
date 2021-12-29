namespace Goal.Demo22.Application.DTO.People.Requests.Validators
{
    public sealed class AddPersonRequestValidator : PersonRequestValidator<AddPersonRequest>
    {
        public AddPersonRequestValidator()
        {
            ValidateFistName();
            ValidateLastName();
            ValidateCpf();
        }
    }
}
