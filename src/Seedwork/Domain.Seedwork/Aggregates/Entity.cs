using System;

namespace Goal.Domain.Aggregates
{
    public abstract class Entity : Entity<Guid>, IEntity
    {
    }
}
