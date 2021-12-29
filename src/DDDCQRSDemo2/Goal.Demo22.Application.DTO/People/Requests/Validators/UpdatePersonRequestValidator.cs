namespace Goal.Demo22.Application.DTO.People.Requests.Validators
{
    public sealed class UpdatePersonRequestValidator : PersonRequestValidator<UpdatePersonRequest>
    {
        public UpdatePersonRequestValidator()
        {
            ValidateFistName();
            ValidateLastName();
            ValidateCpf();
        }
    }
}
