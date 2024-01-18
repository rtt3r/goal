using System;

namespace Goal.Domain.Abstractions.Aggregates;

public abstract class Entity : Entity<string>, IEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
