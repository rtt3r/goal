using System;

namespace Goal.Seedwork.Domain.Aggregates
{
    public abstract class Entity : Entity<Guid>, IEntity
    {
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Entity()
        {
        }
    }
}
