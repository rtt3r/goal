using Goal.Domain.Commands;

namespace Goal.DemoCqrs.Api.Commands
{
    public abstract class PersonCommand<T> : Command<T>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
    }
}
