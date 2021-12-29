using System;
using Goal.Demo2.Api.Application.Commands.Customers;

namespace Goal.Demo2.Api.Application.Validations.Customers
{
    public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand, Guid>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
