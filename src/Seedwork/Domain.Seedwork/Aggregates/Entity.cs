using System;

namespace Goal.Domain.Seedwork.Aggregates
{
    public abstract class Entity : Entity<Guid>, IEntity
    {
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Entity()
        {
        }
    }
}
