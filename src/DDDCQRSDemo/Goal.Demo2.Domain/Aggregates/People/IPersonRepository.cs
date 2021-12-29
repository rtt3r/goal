using Goal.Domain.Aggregates;

namespace Goal.Demo22.Domain.Aggregates.People
{
    public interface IPersonRepository : IRepository<Person, string>
    {
    }
}
