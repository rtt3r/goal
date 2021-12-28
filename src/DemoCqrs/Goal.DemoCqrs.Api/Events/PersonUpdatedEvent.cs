using System;
using Goal.Domain.Events;

namespace Goal.DemoCqrs.Api.Events
{
    public class PersonUpdatedEvent : Event
    {
        public PersonUpdatedEvent(Guid id, string firstName, string lastName, string cpf)
        {
            Id = Guid.NewGuid();
            AggregateId = id;
            FirstName = firstName;
            LastName = lastName;
            Cpf = cpf;
        }

        public Guid Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Cpf { get; }
    }
}
