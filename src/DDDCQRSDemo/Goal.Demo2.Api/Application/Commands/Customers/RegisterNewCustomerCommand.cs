using System;
using Goal.Demo2.Api.Application.Validations.Customers;

namespace Goal.Demo2.Api.Application.Commands.Customers
{
    public class RegisterNewCustomerCommand : CustomerCommand<Guid>
    {
        public RegisterNewCustomerCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
