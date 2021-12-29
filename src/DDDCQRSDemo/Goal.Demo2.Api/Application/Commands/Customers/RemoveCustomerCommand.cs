using System;
using Goal.Demo2.Api.Application.Validations.Customers;

namespace Goal.Demo2.Api.Application.Commands.Customers
{
    public class RemoveCustomerCommand : CustomerCommand
    {
        public Guid Id { get; set; }

        public RemoveCustomerCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
