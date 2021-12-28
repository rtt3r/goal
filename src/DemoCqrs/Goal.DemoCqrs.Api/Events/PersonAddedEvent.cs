using System;
using Goal.Domain.Events;

namespace Goal.DemoCqrs.Api.Events
{
    public class PersonAddedEvent : Event
    {
        public PersonAddedEvent(Guid id, string firstName, string lastName, string cpf)
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
