using System;
using Goal.Infra.Crosscutting.Specifications;

namespace Goal.DemoCqrs.Domain.Aggregates.People
{
    public static class PersonSpecifications
    {
        public static Specification<Person> MatchCpf(string cpf) => new DirectSpecification<Person>(p => p.Cpf.Number == cpf);

        public static Specification<Person> MatchId(Guid personId) => new DirectSpecification<Person>(p => p.Id == personId);
    }
}
