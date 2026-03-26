namespace Goal.Domain.Aggregates;

public abstract class Entity : Entity<string>, IEntity
{
    protected Entity()
    {
        Id = Guid.CreateVersion7().ToString();
    }
}
