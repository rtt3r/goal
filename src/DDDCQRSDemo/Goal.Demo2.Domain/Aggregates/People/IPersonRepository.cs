using Goal.Domain.Seedwork.Aggregates;

namespace Goal.Demo2.Domain.Aggregates.People
{
    public interface IPersonRepository : IRepository<Person, string>
    {
    }
}
