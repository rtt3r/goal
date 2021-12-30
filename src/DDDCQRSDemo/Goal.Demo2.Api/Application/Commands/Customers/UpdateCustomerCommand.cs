using System;
using Goal.Domain.Seedwork.Commands;

namespace Goal.Demo2.Api.Application.Commands.Customers
{
    public class UpdateCustomerCommand : CustomerCommand<ICommandResult>
    {
        public UpdateCustomerCommand(Guid id, string name, string email, DateTime birthDate)
        {
            AggregateId = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }
    }
}
