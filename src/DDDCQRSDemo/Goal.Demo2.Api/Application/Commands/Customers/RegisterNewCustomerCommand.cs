using System;
using Goal.Demo2.Api.Application.Dtos.Customers;
using Goal.Domain.Seedwork.Commands;

namespace Goal.Demo2.Api.Application.Commands.Customers
{
    public class RegisterNewCustomerCommand : CustomerCommand<ICommandResult<CustomerDto>>
    {
        public RegisterNewCustomerCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
    }
}
