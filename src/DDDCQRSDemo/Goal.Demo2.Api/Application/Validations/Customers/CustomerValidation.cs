using Goal.Demo2.Api.Application.Commands.Customers;

namespace Goal.Demo2.Api.Application.Validations.Customers
{
    public abstract class CustomerValidation<TCommand> : CustomerValidation<TCommand, bool>
        where TCommand : CustomerCommand<bool>
    {
    }
}
