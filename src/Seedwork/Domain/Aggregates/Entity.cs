using System;

namespace Goal.Domain.Aggregates;

public abstract class Entity : Entity<string>, IEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
