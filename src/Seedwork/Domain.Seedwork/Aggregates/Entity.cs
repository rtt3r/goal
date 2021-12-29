using System;

namespace Goal.Domain.Seedwork.Aggregates
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        protected Entity() { }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is IEntity item)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return item.Id.Equals(Id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetType().GetHashCode() * 397 ^ Id.GetHashCode();
            }
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right) => !(left == right);
    }
}
