using Goal.Domain.Aggregates;

namespace Goal.DemoCqrsCqrs.Domain.Aggregates.People
{
    public interface IPersonRepository : IRepository<Person, string>
    {
    }
}
