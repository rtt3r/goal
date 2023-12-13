using System;

namespace Goal.Seedwork.Domain.Aggregates;

public abstract class Entity : Entity<string>, IEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
