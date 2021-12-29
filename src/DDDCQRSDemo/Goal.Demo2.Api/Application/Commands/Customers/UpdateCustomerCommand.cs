using System;
using Goal.Demo2.Api.Application.Validations.Customers;

namespace Goal.Demo2.Api.Application.Commands.Customers
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public Guid Id { get; set; }

        public UpdateCustomerCommand(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
