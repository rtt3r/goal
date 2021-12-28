using Goal.Domain.Aggregates;

namespace Goal.Demo.Domain.Aggregates.People
{
    public interface IPersonRepository : IRepository<Person, string>
    {
    }
}
