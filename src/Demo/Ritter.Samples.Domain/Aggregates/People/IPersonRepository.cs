using Ritter.Domain;

namespace Goal.Demo.Domain.Aggregates.People
{
    public interface IPersonRepository : ISqlRepository<Person, string>
    {
    }
}
