using Goal.Infra.Crosscutting.Specifications;

namespace Goal.Demo22.Domain.Aggregates.People
{
    public static class PersonSpecifications
    {
        public static Specification<Person> MatchCpf(string cpf)
        {
            return new DirectSpecification<Person>(p => p.Cpf.Number == cpf);
        }

        public static Specification<Person> MatchId(string personId)
        {
            return new DirectSpecification<Person>(p => p.Id == personId);
        }
    }
}
